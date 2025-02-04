import { Navigate, useParams } from "react-router-dom"
import { useEffect, useState } from "react";
import Perks from "../Perks";
import PhotosUploader from "../PhotosUploader";
import axios from "../api/axios";
import AccountNav from "./AccountNav";
import { toast } from 'react-toastify';

export default function PLacesForm() {
    const {id} = useParams();
    const [title, setTitle] = useState('');
    const [address, setAddress] = useState('');
    const [addedPhotos, setAddedPhotos] = useState([]);
    const [description, setDescription] = useState('');
    const [perks, setPerks] = useState([]);
    const [extraInfo, setExtraInfo] = useState('');
    const [checkIn, setCheckIn] = useState('');
    const [checkOut, setCheckOut] = useState('');
    const [maxGuests, setMaxGuests] = useState(1);
    const [price, setPrice] = useState(100);
    const [redirect, setRedirect] = useState(false);

    useEffect(() => {
      if(id === 0 || id === undefined){
        return;
      }
      axios.get('api/appartments/places/' + id).then(response =>{
        const {data} = response;
        setTitle(data.title);
        setAddress(data.address);
        setAddedPhotos(data.photos);
        setDescription(data.description);
        setPrice(data.price);
        setPerks(data.perks);
        setExtraInfo(data.extraInfo);
        setCheckIn(data.checkIn);
        setCheckOut(data.checkOut);
        setMaxGuests(data.maxGuests);
      })
    }, [id]);
    function inputHeader(text){
        return(
          <h2 className="text-2xl mt-4">{text}</h2>
        );
      } 
      function inputDescription(text){
        return(
          <p className="text-gray-500 text-sm">{text}</p>
        );
      }
      function preInput(header, description){
        return(
          <>
            {inputHeader(header)}
            {inputDescription(description)}
          </>
        );
      }

      async function savePlace(e){
        e.preventDefault();  
        const place = {Title: title, Address: address, Description: description, Price: price, ExtraInfo: extraInfo, CheckIn: checkIn, CheckOut: checkOut, MaxGuests: maxGuests, Perks: perks, Photos: addedPhotos  }
        if(id){
         // update
          const response = await axios.put('api/appartments/updateplace', {id, ...place});
          toast.success("Помешкання успішно змінено")
          setRedirect(true);
        }else{
          //new place
          await axios.post('api/appartments/addnewplace', place);
          toast.success("Помешкання успішно додано")
          setRedirect(true);
        }
    }

    if(redirect){
        return <Navigate to={'/account/places'} />
    }

  return (
    <div className="">
        <AccountNav />
          <form onSubmit={savePlace}>
            {preInput('Назва', 'Назва вашого місця має бути коротким і помітним, як у рекламі')}
            <input type="text" 
                   value={title} onChange={e => setTitle(e.target.value)} placeholder="назва, наприклад: Моя мила квартира" />
            {preInput('Адреса', 'Адреса цього місця')}
            <input type="text" 
                   value={address} onChange={e => setAddress(e.target.value)} placeholder="адреса" />
            {preInput('Фото', 'більше = краще')}
            <PhotosUploader addedPhotos={addedPhotos} onChange={setAddedPhotos}/>
            {preInput('Опис', 'Опис вашого місця')}
            <textarea 
               value={description}
               onChange={e => setDescription(e.target.value)}/>
            {preInput('Бонуси', 'Виберіть усі бажаючі бонуси')}
            <Perks selected={perks} onChange={setPerks}/>     
            {preInput('Додаткова інформація', 'правила будинку тощо.')}
            <textarea 
               value={extraInfo}
               onChange={e => setExtraInfo(e.target.value)} />
            {preInput('Час заїзду та виїзду', 'додайте час заїзду та виїзду, пам’ятайте про деякий час для прибирання кімнат')}
            <div className="grid md:grid-cols-4 grid-cols-2 gap-2">
              <div className="mt-2 -mb-1">
                <h3>Час заїзду</h3>
                <input type="text" 
                      value={checkIn}
                      onChange={e => setCheckIn(e.target.value)}placeholder="14"/>
              </div>
              <div className="mt-2 -mb-1">
              <h3>Час виїзду</h3>
              <input type="text"
                     value={checkOut}
                     onChange={e => setCheckOut(e.target.value)}
                     placeholder="11"/>
              </div>
              <div className="mt-2 -mb-1">
              <h3>Макс. гостей</h3>
              <input type="number"
                     value={maxGuests}
                     onChange={e => setMaxGuests(e.target.value)}
                     placeholder="1"/>
              </div>
              <div className="mt-2 -mb-1">
              <h3>Ціна за ніч</h3>
              <input type="number"
                     value={price}
                     onChange={e => setPrice(e.target.value)}
                     placeholder="100"/>
              </div>
            </div>
              <button className="primary my-4">Зберегти</button>
          </form>
          
        </div>
  )
}
