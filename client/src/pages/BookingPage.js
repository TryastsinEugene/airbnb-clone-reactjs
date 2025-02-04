import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import axios from "../api/axios";
import AddressLink from "../AddressLink";
import PlaceGallery from "../PlaceGallery";
import BookingDates from "../BookingDates";

export default function BookingPage() {
  const {id} = useParams();
  const [booking,setBooking] = useState(null);
  const [appartment, setAppartment] = useState(null);

  useEffect(() => {
    if (id) {
      axios.get('api/booking/bookings/' + id).then(response => {
        const foundBooking = response.data;
         if (foundBooking) {
          setBooking(foundBooking);
          axios.get('api/appartments/places/' + foundBooking.appartmentId).then(response => {
            const foundApp = response.data;
            if(foundApp){
              setAppartment(foundApp);
              console.log(foundApp)
            }

          })
        }
      });
    }
  }, [id]);

  useEffect(() => {
    console.log(appartment)
  }, [appartment])

  if (!booking || !appartment) {
    return '';
  }

 

  return (
    <div className="my-8">
      <h1 className="text-2xl md:text-3xl lg:text-3xl">{appartment.title}</h1>
      <AddressLink className="my-2 block">{appartment.address}</AddressLink>
      <div className="bg-gray-200 p-6 my-6 rounded-2xl flex items-center justify-between">
        <div>
          <h2 className="text-xl md:text-2xl lg:text-2xl mb-4">Інформація про бронювання:</h2>
          <div className="text-[12px] md:text-sm lg:text:sm">
          <BookingDates booking={booking} />
          </div>
        </div>
        <div className="bg-primary p-3 md:p-5 lg:p-6 text-white rounded-2xl">
          <div>Вартість</div>
          <div className="md:text-3xl">${booking.price}</div>
        </div>
      </div>
      <PlaceGallery place={appartment} />
    </div>
  );
}