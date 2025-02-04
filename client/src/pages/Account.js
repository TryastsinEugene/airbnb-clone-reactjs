import { useContext, useState } from "react"
import { UserContext } from "../UserContext"
import {Link, Navigate, useParams} from "react-router-dom"
import axios from "../api/axios";
import PlacesPage from "./PlacesPage";
import AccountNav from "./AccountNav";

const Account = () => {
    const[redirect, setRedirect] = useState(null);
    const {success, user, setUser} = useContext(UserContext);

    let {subpage} = useParams();
    if(subpage === undefined){
        subpage = 'profile';
    }

    async function logout(){
        await axios.post('api/Clients/logout');
        setRedirect('/home');
        setUser(null);
    }

    if(!success){
        return 'Loading...';
    }
    if(success && !user && !redirect){
        return <Navigate to={'/login'} />
    }

   

   if(redirect){
    return <Navigate to={redirect} />
   }
  return (
    <div>
        <AccountNav />
        {subpage === 'profile' && (
            <div className="text-center max-w-lg mx-auto">
                Увійшли як {user.name} ({user.email}) <br />
                <button onClick={logout} className="primary max-w-sm mt-2">Вийти</button>
            </div>
        )}
        {subpage === 'places' && (
            <PlacesPage />
        )}
    </div>
  )
}

export default Account;
