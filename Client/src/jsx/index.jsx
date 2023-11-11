import React, { StrictMode } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { createRoot } from "react-dom/client";
import "../scss/index.scss";
import App from "./app.jsx";
import Login from "./login.jsx";
import Register from "./register.jsx";

const root = createRoot(document.getElementById("root"));


root.render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/*" element={<App/>}/>
        <Route path="/login" element={<Login/>}/>
        <Route path="/register" element={<Register/>}/>
      </Routes>

    </BrowserRouter>
  </StrictMode>
);