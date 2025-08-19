"use client";
import { useEffect } from "react";
import { useRouter } from "next/navigation";

export default function Home() {
  const router = useRouter();

  useEffect(() => {
    const token = typeof window !== "undefined" ? localStorage.getItem("token") : null;
    const role = typeof window !== "undefined" ? localStorage.getItem("role") : null;
    const isLoggedIn = !!token;
    console.log("Token:", token);
    console.log("Role:", role);
     if (role === "Admin") {
      router.replace("/admin");
    } else if (role === "Kullanıcı") {
      router.replace("/user");
    } else {
      router.replace("/login");
    }
  }, [router]);

  return null;
}
