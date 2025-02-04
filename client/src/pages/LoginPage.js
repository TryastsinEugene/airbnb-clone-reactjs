import {Link, Navigate} from "react-router-dom";
import {useRef, useState, useEffect, useContext} from 'react';
import axios from "../api/axios";
import { toast } from 'react-toastify';

import { UserContext } from "../UserContext";

const LOGIN_URL = 'api/Clients/login';

const LoginPage = () => {
  const emailRef = useRef();
  const errRef = useRef();

  const [email, setEmail] = useState('');
  const [pwd, setPwd] = useState('');
  const [errMsg, setErrMsg] = useState('');
  const [redirect, setRedirect] = useState(false);

  const {setUser} = useContext(UserContext);
  // const {setSuccess} = useContext(UserContext);
  useEffect(() => {
    emailRef.current.focus();
  }, [])

  useEffect(() => {
    setErrMsg('');
  }, [email, pwd])

  const handleSubmit = async (e) =>{
    e.preventDefault();
    
    try{
      const {data} = await axios.post(LOGIN_URL, {Email: email, Password: pwd});
      console.log(data);

      setUser(data);
      toast.success('Вітаємо!');
      setRedirect(true);
    }catch(e){
      toast.error('Такого користувача не існує');
    }
    
  }
  if(redirect){
    return <Navigate to={'/account'} />
  }
  return (
    <>
    <section>
      <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
   <div className="mt-4 grow flex items-center justify-around">
        <div className="mb-64">
        <h1 className="text-4xl text-center">Логін</h1>
    <form className="max-w-md mx-auto mt-2" onSubmit={handleSubmit}>
        <input 
            type="email" 
            placeholder="Пошта" 
            ref={emailRef}
            autoComplete="off"
            onChange={(e) => setEmail(e.target.value)}
            value={email}
            required
        />
        <input 
            type="password"
            placeholder="Пароль" 
            onChange={(e) => setPwd(e.target.value)}
            value={pwd}
            required
            />
        <button className="primary">Логін</button>
        <div className="text-center py-2 text-gray-500">
        Ще не маєте облікового запису?
            <Link className="underline text-black" to={'/register'}> Зареєструватись?</Link>
        </div>
    </form>
        </div>
        
    </div>
    </section>
    </>
  )
}

export default LoginPage