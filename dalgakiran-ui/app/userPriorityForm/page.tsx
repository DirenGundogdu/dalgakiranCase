
"use client";
import { Form, Input, Button, Typography, Select, Table } from "antd";
import axios from "axios";
import { useEffect, useState } from "react";

const priorityOptions = [
	{ value: 1, label: "Low" },
	{ value: 2, label: "Medium" },
	{ value: 3, label: "High" },
	{ value: 4, label: "Critical" },
];

export default function UserPriorityFormPage() {
	const [productOptions, setProductOptions] = useState<{ value: string; label: string }[]>([]);
	const [selectedProductId, setSelectedProductId] = useState<string | null>(null);

	useEffect(() => {
		const fetchProducts = async () => {
			const token = localStorage.getItem("token");
			try {
				const response = await axios.get("http://localhost:5261/api/Equipments", {
					headers: {
						"accept": "text/plain",
						"Authorization": `Bearer ${token}`,
					},
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

	const onFinish = (values: any) => {
		// Seçilen ürün id'si: selectedProductId
		console.log("Form values:", { ...values, selectedProductId });
	};

	return (
		<div style={{ maxWidth: 700, margin: "40px auto" }}>
			<Typography.Title level={2}>Kullanıcı Zimmet Talebi</Typography.Title>
			<Form layout="vertical" onFinish={onFinish}>
				<Form.Item label="Ürün" name="product" rules={[{ required: true, message: "Ürün seçin!" }]}> 
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
					<Select options={priorityOptions} placeholder="Aciliyet seçin" />
				</Form.Item>
				<Form.Item>
					<Button type="primary" htmlType="submit" block>Gönder</Button>
				</Form.Item>
			</Form>
		</div>
	);
}