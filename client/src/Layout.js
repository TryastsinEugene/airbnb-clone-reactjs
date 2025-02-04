import Header from "./Header"
import {Outlet} from "react-router-dom";

const Layout = () => {
  return (
    <div className="flex flex-col min-h-screen relative">
      <Header />
        {/* <hr />         */}
        <div className="mx-8">
        <Outlet />
        </div>
    </div>
  )
}

export default Layout