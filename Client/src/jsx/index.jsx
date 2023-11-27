import React, { StrictMode, createContext } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { createRoot } from "react-dom/client";
import "../scss/index.scss";
import App from "./app.jsx";
import Login from "./login.jsx";
import Register from "./register.jsx";
import NewsBlock from "./components/News/newsBlock.jsx";
import Profile from "./components/Profile/profile.jsx";
import NotFound from "./notFound.jsx";
import Upload from "./components/Music/upload.jsx";
import Users from "./components/admin/users.jsx";
import Explore from "./components/Explore/explore.jsx";
import Library from "./components/Profile/library.jsx";
import AlbumView from "./components/Music/albumView.jsx";
import Requests from "./components/admin/Requests/requests.jsx";
import ArtistPage from "./components/Music/artistPage.jsx";
import Subscriptions from "./components/Subscriptions/subscriptions.jsx";

const root = createRoot(document.getElementById("root"));


root.render(
  <StrictMode>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<Login/>}/>
          <Route path="/register" element={<Register/>}/>
          <Route path="/" element={<App/>}>
            <Route index element={<NewsBlock/>}/>
            <Route path="explore" element={<Explore/>}/>
            <Route path="profile" element={<Profile/>}/>
            <Route path="upload" element={<Upload/>}/>
            <Route path="users" element={<Users/>}/>
            <Route path="library" element={<Library/>}/>
            <Route path="requests" element={<Requests/>}/>
            <Route path="album/:id" element={<AlbumView/>}/>
            <Route path="artist/:id" element={<ArtistPage/>}/>
            <Route path="subscriptions" element={<Subscriptions/>}/>
          </Route> 
          <Route path="*" element={<NotFound/>}/>
        </Routes>
      </BrowserRouter>  
  </StrictMode>
);