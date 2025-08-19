"use client";
import { Form, Input, Button, Typography } from "antd";
import axios from "axios";

interface LoginValues {
  email: string;
  password: string;
}

export default function LoginPage() {
  const onFinish = async (values: LoginValues) => {
    try {
      const response = await axios.post(
        "http://localhost:5261/api/Auth/login",
        {
          email: values.email,
          password: values.password,
        },
        {
          headers: {
            "accept": "*/*",
            "Content-Type": "application/json",
          },
        }
      );
  // Başarılı girişte yapılacaklar
  localStorage.setItem("token", response.data.token);
  localStorage.setItem("role", response.data.role);
  window.location.replace("/");
    } catch (error) {
      // Hatalı girişte yapılacaklar
      console.error("Giriş hatası:", error);
    }
  };

  return (
    <div style={{ maxWidth: 400, margin: "40px auto" }}>
      <Typography.Title level={2}>Giriş Yap</Typography.Title>
      <Form layout="vertical" onFinish={onFinish}>
        <Form.Item label="Email" name="email" rules={[{ required: true, type: "email", message: "Geçerli bir email girin!" }]}> 
          <Input />
        </Form.Item>
        <Form.Item label="Şifre" name="password" rules={[{ required: true, message: "Şifre girin!" }]}> 
          <Input.Password />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" block>Giriş Yap</Button>
        </Form.Item>
      </Form>
    </div>
  );
}
