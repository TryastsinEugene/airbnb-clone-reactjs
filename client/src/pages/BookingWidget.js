import { differenceInCalendarDays } from "date-fns";
import { useContext } from "react";
import { useEffect } from "react";
import { useState } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import axios from "../api/axios";
import { UserContext } from "../UserContext";
import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { toast } from 'react-toastify';

const BookingWidget = ({place}) => {
  const [checkIn, setCheckIn] = useState('');
  const [checkOut, setCheckOut] = useState('');

  const [numberOfGuests, setNumberOfGuests] = useState(1);
  const [validNumber, setValidNumber] = useState(true);
  const [numberFocus, setNumberFocus] = useState(false);

  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');
  const [redirect, setRedirect] = useState('');
  const navigate = useNavigate();

  const {user} = useContext(UserContext);

  useEffect(() => {
    if(user){
      setName(user.name);
    }
  }, [user]);
  
  useEffect(() => {
    console.log(numberOfGuests)
    if(numberOfGuests > place.maxGuests || numberOfGuests < 1){
      setValidNumber(false);
    }else{
      setValidNumber(true);
    }
  }, [numberOfGuests])

  let numberOfDays = 0;

  if(checkIn && checkOut){
    numberOfDays = differenceInCalendarDays(checkOut, checkIn);
  }

  async function bookThisPlace(){
    if(!user){
      toast.info("Будь ласка увійдіть у свій аккаунт");
      navigate('/login');
      return;
    }
    const data = {
      CheckIn: checkIn, 
      CheckOut: checkOut,
      NumberOfGuests: numberOfGuests,
      Name: name,
      Phone: phone,
      Price: numberOfDays * place.price ,
      AppartmentId: place.id
    };
    const response = await axios.post('api/booking/addbooking', data);
    console.log(response)
    const bookingUrl = `/account/bookings`;
    toast.success("Помешкання заброньовано")
    setRedirect(bookingUrl);
  }

  if(redirect){
    return <Navigate to={redirect} />
  }
  return ( 
    <div className="bg-white shadow p-4 rounded-2xl">
              <div className="text-2xl text-center">
                  Ціна: ${place.price} / за ніч
              </div>
                 <div className="border rounded-2xl mt-4">
                  <div className="flex">
                    <div className="py-3 px-4">
                     <label>Заїзд: </label>
                     <input type="date" value={checkIn} 
                     onChange={ev => setCheckIn(ev.target.value)}/>
                    </div>
                 <div className="py-3 px-4 border-l">
                     <label>Виїзд: </label>
                    <input type="date" value={checkOut} 
                    onChange={ev => setCheckOut(ev.target.value)}/>
                 </div>
                 </div>
                 <div className="py-3 px-4 border-t">
                    <label>Кількість гостей: </label>
                    <input 
                      type="number"
                      value={numberOfGuests} 
                      onChange={ev => setNumberOfGuests(ev.target.value)}
                      aria-invalid={validNumber ? "false" : "true"}
                      aria-describedby="uidnote"
                      onFocus={() => setNumberFocus(true)}
                      onBlur={() => setNumberFocus(false)}
                />
               <p id="uidnote" className={numberFocus && !validNumber ? "instructions" : "offscreen"}>
              <FontAwesomeIcon icon={faInfoCircle} className="text-primary opacity-60"/>
              <span className="text-primary opacity-60">Максимальна кількість гостей {place.maxGuests}</span>         
               </p>
                 </div>
                 {numberOfDays > 0 && (
                  <div className="py-3 px-4 border-t">
                    <label>Повне ім'я: </label>
                    <input type="text" value={name} onChange={ev => setName(ev.target.value)}/>
                    <label>Номер телефона: </label>
                    <input type="tel" value={phone} onChange={ev => setPhone(ev.target.value)}/>
                  </div>
                 )}
                 </div>
                 {checkIn && checkOut && validNumber ?
                 (                
                  <button onClick={bookThisPlace} className="primary mt-4">
                  Забронювати це місце
                  {numberOfDays > 0 && (
                    <>
                      <span> ${numberOfDays * place.price}</span>
                    </>
                  )}
                </button>
                 ) : (
                  <button onClick={bookThisPlace} className="primary mt-4 cursor-not-allowed" disabled>
                    Забронювати це місце
                    {numberOfDays > 0 && (
                      <>
                        <span> ${numberOfDays * place.price}</span>
                      </>
                    )}
                  </button>
                 )
                    }
                  
    </div>
  )
}

export default BookingWidget