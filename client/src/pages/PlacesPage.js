import { useEffect, useState } from "react";
import { Link } from "react-router-dom"
import axios from "../api/axios";
import AccountNav from "./AccountNav";
import { toast } from 'react-toastify';

const PlacesPage = () => {
  const [places, setPlaces] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchItems = async () => {
      try{
        const response = await axios.get('api/appartments/places');
        setPlaces(response.data);
      } catch(err){
        toast.error(err)
      }finally{
        setIsLoading(false)
      }
      
    }
    fetchItems();
    
  }, []);
  return (
    <div>
     <AccountNav />
     <div className="text-center">
      <Link className="inline-flex gap-1 bg-primary text-white py-2 px-6 rounded-full" to={'/account/places/new'}><svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="size-6">
      <path strokeLinecap="round" strokeLinejoin="round" d="M12 4.5v15m7.5-7.5h-15" />
      </svg>
       Додати помешкання</Link>
     </div>
     <div className="mt-4">
      {places.length > 0 && !isLoading ? places.map(place => (
        <Link to={'/account/places/' + place.id} className="flex mt-2 cursor-pointer gap-4 bg-gray-100 p-4 rounded-2xl lg:mx-24">
          <div className="flex w-32 h-32 bg-gray-300 shrink-0">
          {place.photos.length > 0 && (
          <img className="object-cover w-full" src={place.photos[0]} />
          )}
          </div>
          <div className="flex flex-col grow justify-around">
           <h2 className="text-[16px] leading-tight font-semibold md:text-xl lg:text-xl">
            {place.title}
            </h2>
           <p className="text-sm overflow-hidden text-ellipsis line-clamp-2 mt-4 md:text-xl">
            {place.description}
           </p>
          </div>
        </Link>
      )) : (
          
        <h1 className="text-4xl uppercase font-semibold text-gray-400 text-center mt-48 ">"Знайди свій успіх, допомагаючи іншим знайти їхній дім!"</h1>
        
      )}
     </div>
    </div>
  );
}

export default PlacesPage