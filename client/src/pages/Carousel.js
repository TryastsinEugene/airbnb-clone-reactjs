import React, {useState} from 'react'
import { ChevronLeft, ChevronRight } from 'react-feather'

const Carousel = ({children}) => {
    const [curr, setCurr] = useState(0);

    const prev = (e) =>{
        e.preventDefault();
        setCurr((curr) => (curr === 0 ? children.length - 1 : curr - 1))
        
    }
    const next = (e) =>{
        e.preventDefault();
        setCurr((curr) => (curr === children.length - 1 ? 0 : curr + 1))
    }
  return (
    <div className="relative overflow-hidden group rounded-2xl">
    <div className="flex transition-transform ease-out duration-500" style={{transform: `translateX(-${curr * 100}%)`}}>
        {children}
    </div>
    <div className="absolute inset-0 flex items-center justify-between p-4">
        <button className="opacity-0 group-hover:opacity-70 transition-opacity duration-300 bg-white rounded-full p-2 text-gray-400" onClick={prev}>
            <ChevronLeft size={20} />
        </button>
        <button className="opacity-0 group-hover:opacity-70 transition-opacity duration-300 bg-white rounded-full p-2 text-gray-400" onClick={next}>
            <ChevronRight size={20} />
        </button>
    </div>
    <div className="absolute bottom-4 right-0 left-0">
    <div className="flex items-center justify-center gap-2">
          {children.map((_, i) => (
            <div
              className={`
              transition-all w-2 h-2 bg-white rounded-full
              ${curr === i ? "p-1" : "bg-opacity-50"}
            `}
            />
          ))}
        </div>
    </div>
</div>
    
  )
}

export default Carousel