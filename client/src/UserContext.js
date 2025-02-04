import { useEffect } from "react";
import { createContext, useState } from "react"
import axios from "./api/axios";


export const UserContext = createContext({});

export function UserContextProvider({children}){
    const[user, setUser] = useState(null);
    const[success, setSuccess] = useState(false);
    const[searchQuery, setSearchQuery] = useState('');
    useEffect(() => {
        document.title = "ДімМрії";

        const favicon = document.querySelector("link[rel='icon']") || document.createElement('link');
        favicon.rel = 'icon';
        favicon.href = 'house-solid.svg'; // Шлях до вашої іконки
        document.head.appendChild(favicon);
        if (!user) {
          axios.get('api/Clients/profile').then(({data}) => {
            setUser(data);
            setSuccess(true);
          });
        }
      }, []);
    return(
        <UserContext.Provider value={{user, setUser, success, searchQuery, setSearchQuery}}>
            {children}
        </UserContext.Provider>
    );
}