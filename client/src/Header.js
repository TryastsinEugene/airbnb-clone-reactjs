import { useContext, useState, useEffect } from 'react';
import {Link, useNavigate, useLocation} from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import { UserContext } from './UserContext';

const Header = () => {
  const {user} = useContext(UserContext);
  const {searchQuery, setSearchQuery} = useContext(UserContext);
  const [currSearch, setCurrSearch] = useState("");
  const location = useLocation();
  const navigate = useNavigate();
  
  const handleSearchClick = (e) => {
    setSearchQuery(currSearch);
    console.log(currSearch);
    navigate('/home');
  };

  useEffect(() => {
    setCurrSearch('');
    setSearchQuery('');
  }, [location.pathname])

  return (
    
    <header className="p-4 flex justify-between gap-2 sticky bg-white top-0 w-full z-10 shadow-lg overflow-hidden" >
      <Link to={'/home'}  className="md:flex lg:flex flex items-center gap-1 text-primary">
      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6">
  <path stroke-linecap="round" stroke-linejoin="round" d="m2.25 12 8.954-8.955c.44-.439 1.152-.439 1.591 0L21.75 12M4.5 9.75v10.125c0 .621.504 1.125 1.125 1.125H9.75v-4.875c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125V21h4.125c.621 0 1.125-.504 1.125-1.125V9.75M8.25 21h8.25" />
</svg>
    <span className="font-bold text-xl hidden md:block lg:block">ДімМрії</span>
      </Link>
      <div className="flex gap-2 border border-gray-300 justify-center items-center rounded-full  px-4 shadow-md shadow-gray-300 w-full md:w-72 lg:w-96">
      <input
        className="border-none box-border
        focus:outline-none"
        type="text"
        placeholder="Пошук..."
        value={currSearch}
        onChange={(e) => setCurrSearch(e.target.value)}
      />
        <button className="bg-primary text-white rounded-full p-1 w-6 h-6" onClick={handleSearchClick}>
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-4 h-4">
  <path strokeLinecap="round" strokeLinejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
</svg>

        </button>
      </div>
      
<Link to={user?'/account':'/login'} className="flex items-center gap-2 border border-gray-300 rounded-full py-2 px-4">
      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
  <path strokeLinecap="round" strokeLinejoin="round" d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5" />
</svg>
<div className="bg-gray-500 text-white rounded-full border border-gray-500 overflow-hidden hidden md:block lg:block">

    <svg  xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" className="w-6 h-6 relative top-1">
      <path fillRule="evenodd" d="M7.5 6a4.5 4.5 0 1 1 9 0 4.5 4.5 0 0 1-9 0ZM3.751 20.105a8.25 8.25 0 0 1 16.498 0 .75.75 0 0 1-.437.695A18.683 18.683 0 0 1 12 22.5c-2.786 0-5.433-.608-7.812-1.7a.75.75 0 0 1-.437-.695Z" clipRule="evenodd" />
    </svg>
</div>
      {!!user && (
        <div className="hidden md:block lg:block">
          {user.name}
        </div>
      )}
    </Link>
    
    </header>
  )
}

export default Header