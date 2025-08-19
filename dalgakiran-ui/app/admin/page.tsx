"use client";
import { Table, Typography, Button } from "antd";

const columnsFull = [
  { title: "İsim Soyisim", dataIndex: "fullname", key: "fullname" },
  { title: "Ekipman Adı", dataIndex: "product", key: "product" },
  { title: "Marka", dataIndex: "brand", key: "brand" },
  { title: "Model", dataIndex: "model", key: "model" },
  { title: "Açıklama", dataIndex: "description", key: "description" },
  { title: "Aciliyet", dataIndex: "priority", key: "priority" },
  {
    title: "İşlem",
    key: "action",
    render: (_: any, record: any) => (
      <>
        <Button type="primary" style={{ marginRight: 8 }} onClick={() => onApprove(record.key)}>Onayla</Button>
        <Button danger onClick={() => onReject(record.key)}>Reddet</Button>
      </>
    ),
  },
];

const dataFull = [
  {
    key: "1",
    fullname: "Ali Veli",
    product: "Laptop",
    brand: "Dell",
    model: "XPS 13",
    description: "Acil ihtiyacım var.",
    priority: "Yüksek",
  },
  {
    key: "2",
    fullname: "Ayşe Yılmaz",
    product: "Monitör",
    brand: "Samsung",
    model: "Odyssey",
    description: "Grafik tasarım için.",
    priority: "Orta",
  },
];

const columnsShort = [
  { title: "İsim Soyisim", dataIndex: "fullname", key: "fullname" },
  { title: "Ekipman Adı", dataIndex: "product", key: "product" },
  { title: "Marka", dataIndex: "brand", key: "brand" },
  { title: "Model", dataIndex: "model", key: "model" },
];

const dataShort = [
  { key: "1", fullname: "Ali Veli", product: "Laptop", brand: "Dell", model: "XPS 13" },
  { key: "2", fullname: "Ayşe Yılmaz", product: "Monitör", brand: "Samsung", model: "Odyssey" },
];

function onApprove(key: string) {
  // Onay işlemi burada yapılır
  console.log("Onaylanan talep:", key);
}

function onReject(key: string) {
  // Reddetme işlemi burada yapılır
  console.log("Reddedilen talep:", key);
}

export default function AdminPage() {
  return (
    <div style={{ maxWidth: 1000, margin: "40px auto" }}>
      <Typography.Title level={2}>Talepler Tablosu </Typography.Title>
      <Table columns={columnsFull} dataSource={dataFull} pagination={false} style={{ marginBottom: 40 }} />
      <Typography.Title level={2}>Varlıklar Tablosu</Typography.Title>
      <Table columns={columnsShort} dataSource={dataShort} pagination={false} />
    </div>
  );
}
