import { useState } from "react"
import { useEffect } from "react"
import { Link } from "react-router-dom";
import axios from "../api/axios"
import Carousel from "./Carousel";
import { UserContext } from '../UserContext';
import { useContext } from 'react';
import { toast } from 'react-toastify';

const IndexPage = () => {
  const[places, setPlaces] = useState([]);
  const[filteredPlaces, setFilteredPlaces] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const {searchQuery} = useContext(UserContext);

  useEffect(() => {
    try{
      axios.get('api/appartments/getappartments').then(response =>{
        setPlaces(response.data)
        setFilteredPlaces(response.data)
        if(searchQuery) {
          const result = response.data.filter(place => place.address.toLowerCase().includes(searchQuery.toLowerCase()));
          console.log(result)
          setFilteredPlaces(result);
        }
      });
    }catch(err){
      toast.error(err)
    }finally{
      setIsLoading(false);
    }


    
   
  }, []);

 
  useEffect(() => {
    if(!searchQuery) return setFilteredPlaces(places);
    const result = places.filter(place => place.address.toLowerCase().includes(searchQuery.toLowerCase()) ||
    place.title.toLowerCase().includes(searchQuery.toLowerCase()));
    setFilteredPlaces(result);
  }, [searchQuery])

  
  return (
    <>
    {isLoading && <p className="mx-auto my-auto text-4xl text-gray-500">Завантаження...</p>}
 {/* {filteredPlaces.length === 0 && !isLoading && (
        <h1 className="text-4xl text-gray-400 mx-auto my-auto">Нажаль помешкань не знайдено</h1>
      )} */}
    <div className="grid gap-x-6 gap-y-8 mt-8 md:grid-cols-3 lg:grid-cols-4">
      {filteredPlaces.length > 0 && filteredPlaces.map(place => (
      <Link to={'/place/'+place.id} className="w-96 mx-auto md:w-full lg:w-full">  
        <div className="flex rounded-2xl bg-gray-500">
          {place.photos?.length > 0  && (
            <Carousel>
              {place.photos.map((photo) => (
                <img className="rounded-2xl object-cover aspect-square" src={photo} alt="" />           
              ))}
            </Carousel>         
          )}
        </div>
         <h3 className="font-bold">{place.address}</h3>
         <h2 className="text-sm truncate text-gray-500">{place.title}</h2>
         <div className="mt-1">
          <span className="font-bold">${place.price}</span> за ніч
          </div>
      </Link>
      ))}
    </div>
    </>     
  )
}

export default IndexPage