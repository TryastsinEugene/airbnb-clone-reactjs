import React, {useState} from 'react'

const PlaceGallery = ({place}) => {

    const [showAllPhotos, setShowAllPhotos] = useState(false);

    if(showAllPhotos){
        return(
          <div className="absolute inset-0 bg-black min-h-screen">
            <div className="p-8 bg-black grid gap-4">
             <div>
              <h2 className="text-3xl text-white mr-36">Фото {place.title}</h2>
             <button onClick={() => {setShowAllPhotos(false)}} className="fixed bg-white flex top-8 right-12 gap-2 py-2 px-4 rounded-2xl shadow shadow-black">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6">
      <path fill-rule="evenodd" d="M5.47 5.47a.75.75 0 0 1 1.06 0L12 10.94l5.47-5.47a.75.75 0 1 1 1.06 1.06L13.06 12l5.47 5.47a.75.75 0 1 1-1.06 1.06L12 13.06l-5.47 5.47a.75.75 0 0 1-1.06-1.06L10.94 12 5.47 6.53a.75.75 0 0 1 0-1.06Z" clip-rule="evenodd" />
    </svg>
    Закрити фото
              </button>
             </div> 
              <div className="flex flex-col items-center gap-4">
              {place.photos?.length > 0 && place.photos.map(photo => (
              <div>
                  <img  src={photo} alt="" />
              </div>  
              ))}
              </div>
              
            </div>
            
          </div>
        );
      }
  return (
    <div className="relative">
           <div className="grid gap-2 grid-cols-[2fr_1fr] rounded-3xl overflow-hidden">
            <div>
            {place.photos?.[0] && (
             <img onClick={() => setShowAllPhotos(true)} className="object-cover aspect-square cursor-pointer" src={place.photos?.[0]} alt="" />
             )}
            </div>
           <div className="grid">
            {place.photos?.[1] && (
             <img onClick={() => setShowAllPhotos(true)} className="object-cover aspect-square cursor-pointer" src={place.photos?.[1]} alt="" />
             )}
            {place.photos?.[2] && (
             <img onClick={() => setShowAllPhotos(true)} className="object-cover aspect-square cursor-pointer relative top-2" src={place.photos?.[2]} alt="" />
             )}            
          </div>
         </div>

          <button onClick={() => {setShowAllPhotos(true)} } className="absolute flex gap-2 bottom-2 right-2 py-2 px-4 bg-white shadow-md shadow-gray-500 rounded-2xl">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" class="size-6">
  <path fill-rule="evenodd" d="M1.5 6a2.25 2.25 0 0 1 2.25-2.25h16.5A2.25 2.25 0 0 1 22.5 6v12a2.25 2.25 0 0 1-2.25 2.25H3.75A2.25 2.25 0 0 1 1.5 18V6ZM3 16.06V18c0 .414.336.75.75.75h16.5A.75.75 0 0 0 21 18v-1.94l-2.69-2.689a1.5 1.5 0 0 0-2.12 0l-.88.879.97.97a.75.75 0 1 1-1.06 1.06l-5.16-5.159a1.5 1.5 0 0 0-2.12 0L3 16.061Zm10.125-7.81a1.125 1.125 0 1 1 2.25 0 1.125 1.125 0 0 1-2.25 0Z" clip-rule="evenodd" />
</svg>
Показати більше фото</button>
        </div>
  )
}

export default PlaceGallery