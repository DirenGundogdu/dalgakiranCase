"use client";

import axios from "axios";
import { useEffect, useState } from "react";
import { Table, Typography, Button } from "antd";
import { useRouter } from "next/navigation";


const columns = [
  { title: "Name", dataIndex: "name", key: "name" },
  { title: "Brand", dataIndex: "brand", key: "brand" },
  { title: "Model", dataIndex: "model", key: "model" },
];

export default function UserPage() {
  const [data, setData] = useState<{ key: number; name: string; brand: string; model: string }[]>([]);
  const router = useRouter();

  useEffect(() => {
    const fetchData = async () => {
      const token = localStorage.getItem("token");
      try {
        const response = await axios.get("http://localhost:5261/api/UserEquipments/user", {
          headers: {
            "accept": "text/plain",
            "Authorization": `Bearer ${token}`,
          },
        });
        // Gelen veriyi tabloya uygun şekilde ayarla
        const apiData = response.data;
        const tableData = Array.isArray(apiData)
          ? apiData.map((item: any, idx: number) => ({
              key: idx,
              name: item.equipment?.name ?? "",
              brand: item.equipment?.brand ?? "",
              model: item.equipment?.model ?? "",
            }))
          : [{
              key: 0,
              name: apiData.equipment?.name ?? "",
              brand: apiData.equipment?.brand ?? "",
              model: apiData.equipment?.model ?? "",
            }];
        setData(tableData);
      } catch (error) {
        console.error("Veri çekme hatası:", error);
      }
    };
    fetchData();
  }, []);
  return (
    <div style={{ maxWidth: 800, margin: "40px auto" }}>
      <Button type="primary" style={{ marginBottom: 24 }} onClick={() => router.push("/userPriorityForm")}>Zimmet Talebi Oluştur</Button>
      <Typography.Title level={2}>Zimmetlerim</Typography.Title>
      <Table columns={columns} dataSource={data} pagination={false} />
    </div>
  );
}