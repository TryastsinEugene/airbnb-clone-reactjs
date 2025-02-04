import {Route, Routes} from 'react-router-dom';
import IndexPage from './pages/IndexPage';
import LoginPage from './pages/LoginPage';
import Account  from './pages/Account';
import Layout from './Layout';
import Register from './pages/Register';
import { UserContextProvider } from './UserContext';
import PlacesPage from './pages/PlacesPage';
import PlacePage from './pages/PlacePage';

import PLacesForm from './pages/PLacesForm';
import BookingsPage from './pages/BookingsPage';
import BookingPage from './pages/BookingPage';
import { ToastContainer, Bounce } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
function App() {
  return (
    <UserContextProvider>
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<IndexPage />} />
        <Route path="/home" element={<IndexPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<Register />} />
        <Route path="/account" element={<Account />} />
        <Route path="/account/places" element={<PlacesPage />} />
        <Route path="/account/places/new" element={<PLacesForm />} />
        <Route path="/account/places/:id" element={<PLacesForm />} />
        <Route path="/place/:id" element={<PlacePage />} />
        <Route path="/account/bookings" element={<BookingsPage />} />
        <Route path="/account/bookings/:id" element={<BookingPage />} />
      </Route>
    </Routes>
      <ToastContainer 
      stacked={true}
      autoClose={5000}
      position='bottom-right'
      pauseOnHover={false}
      theme='colored'
      transition={Bounce}
      closeOnClick={true}
      hideProgressBar={false}
      limit={3}/>
    
    </UserContextProvider>
  )
}

export default App;
