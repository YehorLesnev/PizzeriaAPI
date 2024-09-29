"use client"
import { title } from "@/components/primitives";
import ProductList from "@/components/products/product-list";
import {useAppDispatch} from "@/hooks/useStore";
import {useEffect} from "react";
import {setActivePage} from "@/store/features/product/productSlice";

export default function HotDogPage() {
	const dispatch = useAppDispatch()

	useEffect(() => {
		dispatch(setActivePage(1))
	}, []);
	return (
		<div className={"w-[100vh] m-auto"}>
			<h1 className={title()}>Hotdogs</h1>
			<ProductList entity={"HotDog"} />
		</div>
	);
}