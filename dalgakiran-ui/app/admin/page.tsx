"use client";
import { Table, Typography, Button, Input, Space } from "antd";
import { SearchOutlined } from '@ant-design/icons';
import axios from "axios";
import { useEffect, useState, useRef } from "react";
import type { InputRef } from 'antd';
import type { FilterConfirmProps } from 'antd/es/table/interface';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

export default function AdminPage() {
  const [equipments, setEquipments] = useState([]);
  const [requests, setRequests] = useState([]);
  const [chartData, setChartData] = useState<any>(null);
  const token = localStorage.getItem("token");
  const searchInput = useRef<InputRef>(null);

  const handleSearch = (
    selectedKeys: string[],
    confirm: (param?: FilterConfirmProps) => void,
    dataIndex: string,
  ) => {
    confirm();
  };

  const handleReset = (clearFilters: () => void) => {
    clearFilters();
  };

  const getColumnSearchProps = (dataIndex: string) => ({
    filterDropdown: ({ setSelectedKeys, selectedKeys, confirm, clearFilters }: any) => (
      <div style={{ padding: 8 }} onKeyDown={(e) => e.stopPropagation()}>
        <Input
          ref={searchInput}
          placeholder={`Ara ${dataIndex}`}
          value={selectedKeys[0]}
          onChange={(e) => setSelectedKeys(e.target.value ? [e.target.value] : [])}
          onPressEnter={() => handleSearch(selectedKeys as string[], confirm, dataIndex)}
          style={{ marginBottom: 8, display: 'block' }}
        />
        <Space>
          <Button
            type="primary"
            onClick={() => handleSearch(selectedKeys as string[], confirm, dataIndex)}
            icon={<SearchOutlined />}
            size="small"
            style={{ width: 90 }}
          >
            Ara
          </Button>
          <Button
            onClick={() => clearFilters && handleReset(clearFilters)}
            size="small"
            style={{ width: 90 }}
          >
            Temizle
          </Button>
        </Space>
      </div>
    ),
    filterIcon: (filtered: boolean) => (
      <SearchOutlined style={{ color: filtered ? '#1677ff' : undefined }} />
    ),
    onFilter: (value: any, record: any) =>
      record[dataIndex]
        .toString()
        .toLowerCase()
        .includes((value as string).toLowerCase()),
    onFilterDropdownOpenChange: (visible: boolean) => {
      if (visible) {
        setTimeout(() => searchInput.current?.select(), 100);
      }
    },
  });

  const columnsFull = [
    {
      title: "İsim Soyisim",
      dataIndex: "fullname",
      key: "fullname",
      ...getColumnSearchProps('fullname'),
    },
    {
      title: "Ekipman Adı",
      dataIndex: "product",
      key: "product",
      ...getColumnSearchProps('product'),
    },
    {
      title: "Marka",
      dataIndex: "brand",
      key: "brand",
      ...getColumnSearchProps('brand'),
    },
    {
      title: "Model",
      dataIndex: "model",
      key: "model",
      ...getColumnSearchProps('model'),
    },
    { title: "Açıklama", dataIndex: "description", key: "description" },
    { title: "Aciliyet", dataIndex: "priority", key: "priority" },
    {
      title: "Durum",
      dataIndex: "status",
      key: "status",
      filters: [
        { text: 'Beklemede', value: 'Beklemede' },
        { text: 'Kabul Edilmiş', value: 'Kabul Edilmiş' },
        { text: 'Reddedilmiş', value: 'Reddedilmiş' },
      ],
      onFilter: (value: any, record: any) => record.status === value,
    },
    {
      title: "İşlem",
      key: "action",
      render: (_: any, record: any) => (
        record.status === "Beklemede" ? (
          <div className="flex items-center justify-center gap-2">
            <Button type="primary" style={{ marginRight: 8 }} onClick={() => onApprove(record.key, record.userId)}>Onayla</Button>
            <Button danger onClick={() => onReject(record.key, record.userId)}>Reddet</Button>
          </div>
        ) : null
      ),
    },
  ];

  const columnsShort = [
    {
      title: "İsim Soyisim",
      dataIndex: "fullname",
      key: "fullname",
      ...getColumnSearchProps('fullname'),
    },
    {
      title: "Ekipman Adı",
      dataIndex: "product",
      key: "product",
      ...getColumnSearchProps('product'),
    },
    {
      title: "Marka",
      dataIndex: "brand",
      key: "brand",
      ...getColumnSearchProps('brand'),
    },
    {
      title: "Model",
      dataIndex: "model",
      key: "model",
      ...getColumnSearchProps('model'),
    },
  ];

  const getPriorityLabel = (priority: number) => {
    switch (priority) {
      case 1: return "Düşük";
      case 2: return "Orta";
      case 3: return "Yüksek";
      case 4: return "Kritik";
      default: return "Bilinmiyor";
    }
  };

  const getStatusLabel = (status: number) => {
    switch (status) {
      case 1: return "Beklemede";
      case 2: return "Kabul Edilmiş";
      case 3: return "Reddedilmiş";
      default: return "Bilinmiyor";
    }
  };

  const fetchProducts = async () => {
    try {
      const response = await axios.get("http://localhost:5261/api/Equipments", {
        headers: {
          "accept": "text/plain",
          "Authorization": `Bearer ${token}`,
        }
        ,
      });
      const formattedData = response.data.map((item: any) => {

        const userEquipment = item.userEquipments && item.userEquipments.length > 0 ? item.userEquipments[0] : null;
        const user = userEquipment ? userEquipment.user : null;

        return {
          key: item.id,
          fullname: user ? `${user.firstName} ${user.lastName}` : "Atanmamış",
          product: item.name,
          brand: item.brand,
          model: item.model
        };
      });

      setEquipments(formattedData);

      // Chart datası oluştur - Kullanıcı bazında ekipman sayıları
      const userEquipmentCounts: { [key: string]: number } = {};
      response.data.forEach((item: any) => {
        const userEquipment = item.userEquipments && item.userEquipments.length > 0 ? item.userEquipments[0] : null;
        const user = userEquipment ? userEquipment.user : null;

        if (user) {
          const userName = `${user.firstName} ${user.lastName}`;
          userEquipmentCounts[userName] = (userEquipmentCounts[userName] || 0) + 1;
        }
      });

      const chartLabels = Object.keys(userEquipmentCounts);
      const chartValues = Object.values(userEquipmentCounts);

      setChartData({
        labels: chartLabels,
        datasets: [
          {
            label: 'Kullanıcı Başına Ekipman Sayısı',
            data: chartValues,
            backgroundColor: [
              'rgba(255, 99, 132, 0.6)',
              'rgba(54, 162, 235, 0.6)',
              'rgba(255, 205, 86, 0.6)',
              'rgba(75, 192, 192, 0.6)',
              'rgba(153, 102, 255, 0.6)',
              'rgba(255, 159, 64, 0.6)',
              'rgba(255, 193, 7, 0.6)',
              'rgba(76, 175, 80, 0.6)',
            ],
            borderColor: [
              'rgba(255, 99, 132, 1)',
              'rgba(54, 162, 235, 1)',
              'rgba(255, 205, 86, 1)',
              'rgba(75, 192, 192, 1)',
              'rgba(153, 102, 255, 1)',
              'rgba(255, 159, 64, 1)',
              'rgba(255, 193, 7, 1)',
              'rgba(76, 175, 80, 1)',
            ],
            borderWidth: 1,
          },
        ],
      });
    } catch (error) {
      console.error("Ekipmanlar çekilemedi:", error);
    }
  }
  const fetchRequest = async () => {
    try {
      const response = await axios.get("http://localhost:5261/api/UserEquipmentRequests", {
        headers: {
          "accept": "text/plain",
          "Authorization": `Bearer ${token}`,
        }
        ,
      });
      const formattedRequests = response.data.map((request: any) => ({
        key: request.id,
        fullname: `${request.user.firstName} ${request.user.lastName}`,
        product: request.equipment.name,
        brand: request.equipment.brand,
        model: request.equipment.model,
        description: request.description,
        priority: getPriorityLabel(request.priority),
        status: getStatusLabel(request.status),
        equipmentId: request.equipmentId,
        userId: request.userId
      }));

      setRequests(formattedRequests);
    } catch (error) {
      console.error("Ekipmanlar çekilemedi:", error);
    }
  }
  useEffect(() => {
    fetchProducts();
    fetchRequest();
  }, [])

  const onApprove = async (key: string, userId: string) => {
    try {
      const response = await axios.post(
        'http://localhost:5261/api/UserEquipmentRequests/update-status',
        {
          requestId: key,
          status: 2,
          userId: userId
        },
        {
          headers: {
            'accept': '*/*',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
          }
        }
      );

      console.log("Talep onaylandı:", response.data);

      fetchProducts();
      fetchRequest();

    } catch (error) {
      console.error("Onay işlemi başarısız:", error);
    }
  };

  const onReject = async (key: string, userId: string) => {
    try {
      const response = await axios.post(
        'http://localhost:5261/api/UserEquipmentRequests/update-status',
        {
          requestId: key,
          status: 3,
          userId: userId
        },
        {
          headers: {
            'accept': '*/*',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
          }
        }
      );

      console.log("Talep reddedildi:", response.data);

      fetchProducts();
      fetchRequest();

    } catch (error) {
      console.error("Reddetme işlemi başarısız:", error);
    }
  };

  const chartOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top' as const,
      },
      title: {
        display: true,
        text: 'Kullanıcı Başına Ekipman Dağılımı',
      },
    },
    scales: {
      y: {
        beginAtZero: true,
        ticks: {
          stepSize: 1,
        },
      },
    },
  };

  return (
    <div style={{ maxWidth: 1200, margin: "40px auto" }}>
      <Typography.Title level={2}>Ekipman İstatistikleri</Typography.Title>
      {chartData && (
        <div style={{ marginBottom: 40, height: '400px' }}>
          <Bar data={chartData} options={chartOptions} />
        </div>
      )}

      <Typography.Title level={2}>Talepler Tablosu </Typography.Title>
      <Table columns={columnsFull} dataSource={requests} pagination={false} style={{ marginBottom: 40 }} />
      <Typography.Title level={2}>Varlıklar Tablosu</Typography.Title>
      <Table columns={columnsShort} dataSource={equipments} pagination={false} />
    </div>
  );
}
