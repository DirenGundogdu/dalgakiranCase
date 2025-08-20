
"use client";
import { Form, Input, Button, Typography, Select, Table } from "antd";
import axios from "axios";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

const priorityOptions = [
	{ value: 1, label: "Low" },
	{ value: 2, label: "Medium" },
	{ value: 3, label: "High" },
	{ value: 4, label: "Critical" },
];

interface UserPriorityFormValues {
	equipmentId: string;
	description: string;
	priority: number;
}

export default function UserPriorityFormPage() {
	const [productOptions, setProductOptions] = useState<{ value: string; label: string }[]>([]);
	const [selectedProductId, setSelectedProductId] = useState<string | null>(null);
	const [selectedPriority, setSelectedPriority] = useState<number | null>(null);
	const token = localStorage.getItem("token");
	const router = useRouter();

	useEffect(() => {
		const fetchProducts = async () => {
			try {
				const response = await axios.get("http://localhost:5261/api/UnassignedEquipments", {
					headers: {
						"accept": "text/plain",
						"Authorization": `Bearer ${token}`,
					}
					,
				});
				const options = response.data.map((item: any) => ({
					value: item.id,
					label: `${item.name} ${item.brand} ${item.model}`,
				}));
				setProductOptions(options);
				
			} catch (error) {
				console.error("Ürünler çekilemedi:", error);
			}
		};
		fetchProducts();
	}, []);

	const onFinish = (values: UserPriorityFormValues) => {
		try {
			const response = axios.post(
				 "http://localhost:5261/api/UserEquipmentRequests/create",
				 {
					 equipmentId: values.equipmentId,
					 description: values.description,
					 priority: values.priority
				 },{
				 headers: {
						"accept": "text/plain",
						"Authorization": `Bearer ${token}`,
					}
				}
				 
			)
			console.log("Response : " + response);
			   setTimeout(() => {
                router.push("/user");
            }, 1000);
		} catch (error) {
			console.error("Hata:", error);
		}


	};

	return (
		<div style={{ maxWidth: 700, margin: "40px auto" }}>
			<Typography.Title level={2}>Kullanıcı Zimmet Talebi</Typography.Title>
			<Form layout="vertical" onFinish={onFinish}>
				<Form.Item label="Ürün" name="equipmentId" rules={[{ required: true, message: "Ürün seçin!" }]}> 
					<Select
						options={productOptions}
						placeholder="Ürün seçin"
						onChange={value => setSelectedProductId(value)}
						showSearch
					/>
				</Form.Item>
				<Form.Item label="Açıklama" name="description" rules={[{ required: true, message: "Açıklama girin!" }]}> 
					<Input.TextArea rows={3} placeholder="Açıklama yazın" />
				</Form.Item>
				<Form.Item label="Aciliyet" name="priority" rules={[{ required: true, message: "Aciliyet seçin!" }]}> 
					<Select options={priorityOptions} onChange={value => setSelectedPriority(value)} placeholder="Aciliyet seçin" />
				</Form.Item>
				<Form.Item>
					<Button type="primary" htmlType="submit" block>Gönder</Button>
				</Form.Item>
			</Form>
		</div>
	);
}