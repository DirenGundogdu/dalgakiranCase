"use client";
import { Geist, Geist_Mono } from "next/font/google";
import "../app/globals.css";
import { Provider } from "react-redux";
import { store } from "../store/store";



export default function ClientLayout({ children }: { children: React.ReactNode }) {
  return (
    <body >
      <Provider store={store}>
        {children}
      </Provider>
    </body>
  );
}
