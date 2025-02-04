
const PlaceImage = ({booking}) => {
    if(!booking.photo){
        return '';
    }

  return (
    <img className='object-cover h-full' src={booking.photo} alt={booking.title}/>
  )
}

export default PlaceImage