import { X } from "react-feather"

export default function Modal({ open, onClose, children }) {
  return (
    // backdrop
    <div
      onClick={(e) => {e.preventDefault();onClose();}}
      className={`overflow-hidden
        fixed inset-0 flex justify-center items-center transition-colors 
        ${open ? "visible bg-black/20" : "invisible"}
      `}
    >
      {/* modal */}
      <div
        onClick={(e) => {e.preventDefault();onClose();}}
        className={`overflow-y-auto
          bg-white rounded-xl shadow p-12 transition-all           ${open ? "scale-100 opacity-100" : "scale-125 opacity-0"}
        `}
      >
        <button
          onClick={(e) => {e.preventDefault();onClose();}}
          className="absolute top-0 left-0 p-1 rounded-lg text-gray-400 bg-white hover:bg-gray-50 hover:text-gray-600"
        >
          <X />
        </button>
        {children}
      </div>
    </div>
  )
}