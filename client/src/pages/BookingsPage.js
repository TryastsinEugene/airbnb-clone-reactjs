import { useEffect } from "react";
import { useState } from "react"
import AccountNav from "./AccountNav"
import axios from "../api/axios";
import PlaceImage from "./PlaceImage";
import { Link, useNavigate } from "react-router-dom";
import BookingDates from "../BookingDates";
import Modal from "./Modal";
import { toast } from 'react-toastify';

const BookingsPage = () => {
  const [bookings, setBookings] = useState([]);
  const [open, setOpen] = useState(false);
  const [deleteId, setDeleteId] = useState();
  const [isLoading, setIsLoading] = useState(true);

  const navigate = useNavigate();

  useEffect(() => {
    const fetchItems = async () => {
      try{
        const response = await axios.get('api/booking/bookingsbyid');
        setBookings(response.data);
      } catch(err){
        toast.error(err)
      }finally{
        setIsLoading(false)
      }
      
    }
    fetchItems();
    
  },[]);  

  const  deleteBooking = async (e, id) =>{
    e.preventDefault();
    setDeleteId(id);
    setOpen(true);
   
  }
  const confirmDelete = async (e) =>{
    e.preventDefault();
    setOpen(false);
    await axios.delete('api/booking/' + deleteId)
    setBookings((prevBookings) => prevBookings.filter((booking) => booking.id !== deleteId));
    toast.success("Бронювання успішно видалено")
  }
  const cancelDelete = async (e) =>{
    e.preventDefault();
    setOpen(false);
  }
  return (
    <div className="lg:mx-12">
        <AccountNav />
        {bookings?.length > 0 && !isLoading ? bookings.map(booking =>(
            <Link to={`/account/bookings/${booking.id}`} className="flex gap-4 bg-gray-200 rounded-2xl overflow-hidden mt-2 relative">
            <div className="w-48 min-h-full">
                <PlaceImage booking={booking} />  
            </div>
            <div className="py-1 md:py-3 pr-3 grow flex flex-col  justify-around">
              <h2 className="text-ellipsis line-clamp-1 md:text-xl lg:text-xl">{booking.appartmentTitle 
}</h2>
              <div className="text-xl flex flex-col justify-between">
                <BookingDates booking={booking} className=" text-gray-500 text-[12px] md:text-[14px] lg:text-xl" />
                <div className="flex items-center gap-1 text-[16px] ml-2 lg:text-xl">
                  <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                    <path strokeLinecap="round" strokeLinejoin="round" d="M2.25 8.25h19.5M2.25 9h19.5m-16.5 5.25h6m-6 2.25h3m-3.75 3h15a2.25 2.25 0 002.25-2.25V6.75A2.25 2.25 0 0019.5 4.5h-15a2.25 2.25 0 00-2.25 2.25v10.5A2.25 2.25 0 004.5 19.5z" />
                  </svg>
                  <span>
                    Вартість: ${booking.price}
                  </span>
                  <button onClick={(e) => deleteBooking(e, booking.id)} className="absolute right-2 bottom-2 bg-red-500 text-white p-1 rounded-md">
                  <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-5 md:size-6">
  <path stroke-linecap="round" stroke-linejoin="round" d="m14.74 9-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 0 1-2.244 2.077H8.084a2.25 2.25 0 0 1-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 0 0-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 0 1 3.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 0 0-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 0 0-7.5 0" />
</svg>
                  </button>
                  <Modal open={open} onClose={() => setOpen(false)}>
        <div className="text-center w-56">
          <div className="mx-auto my-4 w-48">
            <h3 className="text-lg font-black text-gray-800">Підтверження видалення</h3>
            <p className="text-sm text-gray-500">
              Ви дійсно хочете зняти бронювання цього помешкання ?
            </p>
          </div>
          <div className="flex gap-4">
            <button onClick={confirmDelete} className="btn bg-primary shadow p-1 text-white rounded-md  w-full md:text-[18px]">Видалити</button>
            <button
              className="btn p-1 rounded-md shadow w-full md:text-[18px]"
              onClick={cancelDelete}
            >
              Відміна
            </button>
          </div>
        </div>
      </Modal>
                </div>
              </div>
            </div>
            </Link>
            
        )) : (
          
          <h1 className="text-4xl uppercase font-semibold text-gray-400 text-center mt-48 ">"Втільте мрію про ідеальне місце – забронюйте прямо зараз!"</h1>
          
        )}
    </div>
  )
}

export default BookingsPage