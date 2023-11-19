import React, { StrictMode } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { createRoot } from "react-dom/client";
import "../scss/index.scss";
import App from "./app.jsx";
import Login from "./login.jsx";
import Register from "./register.jsx";
import DataBlock from "./components/dataBlock.jsx";
import Profile from "./components/profile.jsx";
import NotFound from "./notFound.jsx";
import Upload from "./components/Music/upload.jsx";
import Users from "./components/admin/users.jsx";
import Explore from "./components/explore.jsx";

const root = createRoot(document.getElementById("root"));



root.render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login/>}/>
        <Route path="/register" element={<Register/>}/>
        <Route path="/" element={<App/>}>
          <Route index element={<DataBlock/>}/>
          <Route path="explore" element={<Explore/>}/>
          <Route path="profile" element={<Profile/>}/>
          <Route path="upload" element={<Upload/>}/>
          <Route path="users" element={<Users/>}/>
        </Route>
        <Route path="*" element={<NotFound/>}/>
      </Routes>

    </BrowserRouter>
  </StrictMode>
);