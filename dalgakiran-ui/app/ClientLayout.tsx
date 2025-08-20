"use client";
import { Geist, Geist_Mono } from "next/font/google";
import "../app/globals.css";
import { Provider } from "react-redux";
import { store } from "../store/store";
import { Button } from "antd";
import { LogoutOutlined } from "@ant-design/icons";
import { useRouter, usePathname } from "next/navigation";
import { useEffect, useState } from "react";

export default function ClientLayout({ children }: { children: React.ReactNode }) {
  const router = useRouter();
  const pathname = usePathname();
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [userRole, setUserRole] = useState<string | null>(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const role = localStorage.getItem("role");
    setIsLoggedIn(!!token);
    setUserRole(role);
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    
    sessionStorage.clear();
    
    router.replace("/login");
  };

  const showLogoutButton = isLoggedIn && pathname !== "/login" && pathname !== "/";

  return (
    <body>
      <Provider store={store}>
        {showLogoutButton && (
          <header style={{ 
            position: "fixed", 
            top: 0, 
            right: 0, 
            zIndex: 1000, 
            padding: "16px",
            background: "rgba(255, 255, 255, 0.9)",
            backdropFilter: "blur(10px)",
            borderRadius: "0 0 0 8px"
          }}>
            <div style={{ display: "flex", alignItems: "center", gap: "12px" }}>
              {userRole && (
                <span style={{ fontSize: "14px", color: "#666" }}>
                  {userRole === "Admin" ? "Admin" : "Kullanıcı"}
                </span>
              )}
              <Button 
                type="primary" 
                danger 
                icon={<LogoutOutlined />}
                onClick={handleLogout}
                size="small"
              >
                Çıkış
              </Button>
            </div>
          </header>
        )}
        <main style={{ paddingTop: showLogoutButton ? "60px" : "0" }}>
          {children}
        </main>
      </Provider>
    </body>
  );
}
