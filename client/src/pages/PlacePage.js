import React from 'react'
import { useEffect } from 'react';
import { useState } from 'react';
import { useParams } from 'react-router-dom'
import AddressLink from '../AddressLink';
import axios from '../api/axios';
import PlaceGallery from '../PlaceGallery';
import BookingWidget from './BookingWidget';
import Modal from './Modal';
const PlacePage = () => {

  const {id} = useParams();
  const [place, setPlace] = useState(null);   
  const [open, setOpen] = useState(false);
  const [isDescription, setIsDescription] = useState(false);

  useEffect(() => {
    if(!id){
        return;
    }
    axios.get('api/appartments/places/' + id).then((response) => {
        setPlace(response.data);
    });
  }, [id]); 

  useEffect(() =>{
    console.log(isDescription);
  }, [isDescription])
  if(!place) return '';

 
  return (
    <div className="mt-8 bg-gray-100 -mx-8 px-8 py-8">
        <h1 className="text-2xl">{place.title}</h1>
          <AddressLink>{place.address}</AddressLink>
          <PlaceGallery place={place} />
        <div className="mt-4 grid gap-8 grid-cols-1 md:grid-cols-[2fr_1fr]">
              <div className="flex flex-col justify-between">
              <div className="my-4">
                <h2 className="font-semibold text-xl">Опис</h2>
                <div className="overflow-hidden text-ellipsis line-clamp-3">
                   {place.description}
                </div>               
                <label className="font-medium text-[16px] cursor-pointer hover:underline" onClick={() => setOpen(true)}>Показати більше &gt;</label>
                <Modal open={open} onClose={() => setOpen(false)}>
        <div className="text-center w-72 h-64  md:w-96 md:h-80">
          <div className="mx-auto my-4 w-full">
          <h3 className="text-lg font-semibold text-gray-800">Про це помешкання</h3>
            <h2 className="m-2 leading-tight">{place.title}</h2>
            <p className="text-sm text-gray-500">
              {place.description}
            </p>
          </div>
          </div>
          </Modal>
                
              </div> 
              <div>
              <h2 className="font-semibold text-xl mt-4">Умови оренди помешкання</h2>
              <div>
                Заїзд о: {place.checkIn}-ї години <br/>
                Виїзд о: {place.checkOut}-ї години <br/>
                Макс. гостей: {place.maxGuests} <br/>
              </div>
               
              </div>
              <div>
        <h2 className="font-semibold text-xl mt-4">Додаткова інформація</h2>
        <div className="text-sm text-gray-700 leading-4 mt-1 overflow-hidden text-ellipsis line-clamp-3"
        >
                  {place.extraInfo}
        </div>
        <label className="font-medium text-[16px] cursor-pointer hover:underline" onClick={() => setIsDescription(true)}>Показати більше &gt;</label>

        <Modal open={isDescription} onClose={() => setIsDescription(false)}>
        <div className="text-center w-72 h-64  md:w-96 md:h-80">
          <div className="mx-auto my-4 w-full">
            <h3 className="text-lg font-semibold text-gray-800">Додаткова інформація</h3>
            <h2 className="m-2 leading-tight">{place.title}</h2>
            <p className="text-sm text-gray-500">
              {place.extraInfo}
            </p>
          </div>
          </div>
          </Modal>
        </div>
              </div>
              <div>
                <BookingWidget place={place} />
              </div>
        </div>
        
    </div>

  )
}

export default PlacePage