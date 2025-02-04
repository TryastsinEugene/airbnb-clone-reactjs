import { useRef, useEffect, useState } from "react";
import {Link} from "react-router-dom";
import { faEye, faEyeSlash, faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { toast } from 'react-toastify';
import axios from "../api/axios";
//import axios from "axios";

import LoginPage from "./LoginPage";
// import axios from "..api/axios";

const USER_REGEX = /^[А-ЯЇЄІҐ][а-яїєіґ]{1,23}$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,24}$/;
const REGISTER_URL = 'api/Clients/register';

const Register = () => {
  const userRef = useRef();
  const errRef = useRef();

  const [name, setName] = useState('');
  const [validName, setValidName] = useState(false);
  const [nameFocus, setNameFocus] = useState(false);

  const [email, setEmail] = useState('');
  // const [valemail, setEmail] = useState('');
  // const [email, setEmail] = useState('');

  const [password, setPassword] = useState('');
  const [validPwd, setValidPwd] = useState(false);
  const [pwdFocus, setPwdFocus] = useState(false);
 
  const [matchPwd, setMatchPwd] = useState('');
  const [validMatch, setValidMatch] = useState(false);
  const [matchFocus, setMatchFocus] = useState(false);

  const [errMsg, setErrMsg] = useState('');
  const [success, setSuccess] = useState(false);

  const [showPwd, setShowPwd] = useState(false);
  const [showMatch, setShowMatch] = useState(false);

  useEffect(() => {
    userRef.current.focus();
  }, [])

  useEffect(() => {
    const result = USER_REGEX.test(name);
    setValidName(result);
  }, [name])
 
  useEffect(() => {
    const result = PWD_REGEX.test(password);
    setValidPwd(result);
    const match = password === matchPwd;
    setValidMatch(match);
  }, [password, matchPwd])

  useEffect(() => {
    setErrMsg('');
  }, [name, password, matchPwd])

  const handleSubmit = async (e) => {
    e.preventDefault();
    // if button enabled with JS hack
    const v1 = USER_REGEX.test(name);
    const v2 = PWD_REGEX.test(password);
    if (!v1 || !v2) {
        setErrMsg("Invalid Entry");
        return;
    }
    try{
      await axios.post(REGISTER_URL,{Name: name, Email: email, Password: password});
      setSuccess(true);
      toast.success("Ви успішно зареєстровані")
    }catch(err){
      if(!err?.response){
        toast.error('Немає відповіді сервера')
    } else if (err.response?.status === 409){
      toast.error('Ця електронна адреса вже зареєстрована')
    } else {
      toast.error('Помилка реєстрації')
    }
    errRef.current.focus();
    }
  }
  return (
    <>
    {success ? (
      <LoginPage />
    ) : (
    <section className="mt-4 grow flex items-center justify-around">
        <div className="mb-64">
        <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
        <h1 className="text-4xl text-center">Реєстрація</h1>
        {/* Форма */}
    <form className="max-w-md mx-auto mt-2" onSubmit={handleSubmit}>
      {/* Ім'я */}
        <input type="text" 
                placeholder="Ваше ім'я" 
                ref={userRef}
                autoComplete="off"
                onChange={e => setName(e.target.value)}
                required
                aria-invalid={validName ? "false" : "true"}
                aria-describedby="uidnote"
                onFocus={() => setNameFocus(true)}
                onBlur={() => setNameFocus(false)}/>
        <p id="uidnote" className={nameFocus && name && !validName ? "instructions" : "offscreen"}>
          <FontAwesomeIcon icon={faInfoCircle} className="text-primary opacity-60"/>
          <span className="text-primary opacity-60"> Від 4 до 24 символів.
          Має починатися з веикої літери.
          Дозволені літери, цифри.</span>         
        </p>
        {/* Пошта */}
        <input type="email" 
                placeholder="Пошта"
                value={email}
                onChange={e => setEmail(e.target.value)}
                required />
        {/* Пароль */}
                <div className="relative w-full">
                <input 
                 placeholder="Пароль" 
                 type={showPwd ? "text" : "password"}
                 autoComplete="off"
                 value={password}
                 onChange={(e) => setPassword(e.target.value)}
                 required
                 aria-invalid={validPwd ? "false" : "true"}
                 aria-describedby="pwdnote"
                 onFocus={() => setPwdFocus(true)}
                 onBlur={() => setPwdFocus(false)}
             />         
             <button 
                    className="absolute right-2 top-3 text-gray-600 bg-white"
                    type="button"
                    onClick={() => setShowPwd(!showPwd)}
                    onFocus={() => setPwdFocus(true)}
                    onBlur={() => setPwdFocus(false)}
                    >
                        <FontAwesomeIcon icon={showPwd ? faEyeSlash : faEye} />
                    </button>
                </div>
                
             <p id="pwdnote" className={pwdFocus && !validPwd ? "instructions" : "offscreen"}>
                             <FontAwesomeIcon icon={faInfoCircle} className="text-primary opacity-60"/>
                             <span className="text-primary opacity-60">
                             Від 8 до 24 символів.<br />
                             Має містити великі та малі літери, цифру та спеціальний символ.
                             </span>
                         </p>
      {/* Підтверження паролю */}
      <div className="relative w-full">
      <input
                 type={showMatch ? "text" : "password"}
                onChange={(e) => setMatchPwd(e.target.value)}
                placeholder="Підтвердження паролю"
                required
                aria-invalid={validMatch ? "false" : "true"}
                aria-describedby="confirmnote"
                onFocus={() => setMatchFocus(true)}
                onBlur={() => setMatchFocus(false)}
            />
            <button 
                    className="absolute right-2 top-3 text-gray-600 bg-white"
                    type="button"
                    onClick={() => setShowMatch(!showMatch)}
                    onFocus={() => setMatchFocus(true)}
                    onBlur={() => setMatchFocus(false)}
                    >
                        <FontAwesomeIcon icon={showMatch ? faEyeSlash : faEye} />
                    </button>
      </div>
          
            <p id="confirmnote" className={matchFocus && !validMatch ? "instructions" : "offscreen"}>
                <FontAwesomeIcon icon={faInfoCircle} className="text-primary opacity-60"/>
                <span className="text-primary opacity-60">
                Має збігатися з першим полем введення пароля.
                </span>
            </p>                         
        <button disabled={!validName || !validPwd|| !validMatch ? true : false} className="primary">Реєстрація</button>
        <div className="text-center py-2 text-gray-500">
            Вже зареєстровані? 
            <Link className="underline text-black" to={'/login'}> Логін </Link>
        </div>
    </form>
        </div>
        
    </section>
    )}
    </>
  )
}

export default Register