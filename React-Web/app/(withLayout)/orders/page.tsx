"use client"
import React, {useEffect, useState} from "react";
import {
    Table,
    TableHeader,
    TableColumn,
    TableBody,
    TableRow,
    TableCell,
    User,
    Chip,
    Tooltip,
    getKeyValue,
    user
} from "@nextui-org/react";
import {CheckIcon, DeleteIcon, EditIcon, EyeIcon} from "@nextui-org/shared-icons";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {setActivePage, setTotalCount} from "@/store/features/product/productSlice";
import {deleteCustomer, getCustomerCount, getOrdersCount} from "@/store/services/customerService";
import ProductPagination from "@/components/pagination/product-pagination";
import {Loader} from "@/components/loader/loader";
import AdminModal from "@/components/modal/admin-modal";
import {DecodedToken} from "@/components/navbar/navbar";
import {getOrders} from "@/store/services/productService";
import {IOrders} from "@/store/features/orders/ordersSlice";
import {IconForbid} from "@tabler/icons-react";
import {BiCheck} from "react-icons/bi";
import {FaRegCheckCircle} from "react-icons/fa";
import {GiCancel} from "react-icons/gi";

const statusColorMap: any = {
    active: "success",
    paused: "danger",
    vacation: "warning",
};
interface Column {
    name: string,
    uid: string
}
const columns: Column[] = [
    {name: "DELIVERY", uid: "delivery"},
    {name: "CATEGORY", uid: "category"},
    {name: "ITEMS", uid: "items"},
    {name: "QUANTITY", uid: "quantity"},
    {name: "SIZE", uid: "size"},
    {name: "DATE", uid: "date"},
    {name: "TOTAL", uid: "total"},

];



export {columns};

const Orders = () => {
    const dispatch = useAppDispatch()
    const {orders} = useAppSelector(
        (state) => state.order,
    );
    const {activePage, limit} = useAppSelector(state => state.product)
    const [email, setEmail] = useState<string>("")
    const {user} = useAppSelector(state => state.user)

    useEffect(() => {
        if (user) {
            setEmail(user[DecodedToken.Email])
        }

    }, [user]);

    useEffect(() => {
        if (email.length) {
            const getOrdersCounter = async () => {
                const res =  getOrdersCount({pageNumber: null, pageSize: null, userEmail: email})
                res.then(res => { console.log(res)
                    let count = 0
                    res.map(() => count++)

                    dispatch(setTotalCount(count))
                })
            }
            getOrdersCounter()
        }


    }, [email]);

    useEffect(() => {
        dispatch(setActivePage(1))

    }, []);
    console.log(orders, "orders")
    useEffect(() => {
        if(email.length){
            dispatch(
                getOrders({pageNumber: activePage, pageSize: limit, userEmail: email }),
            );
        }
       
    }, [dispatch, activePage, limit, email]);



    const renderCell = React.useCallback((order: IOrders, columnKey: any) => {
        const cellValue = order[columnKey];
        const date = order.date.split("T")
        switch (columnKey) {
            case "size":
                return (
                    <div className="relative flex items-end gap-2">

                        {order.orderItems.map(item => (
                            <>{item.item.itemSize} </>
                        )) }

                    </div>
                );
            case "delivery":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{order.delivery ? <FaRegCheckCircle size={20} color={"green"}/> : <GiCancel size={20} color={"red"} />}</p>
                    </div>
                );
            case "quantity":
                return (
                    <div className="relative flex items-end gap-2">

                        {order.orderItems.map(item => (
                            <>{item.quantity} </>
                        )) }

                    </div>
                );
            case "date":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{date[0]}</p>
                    </div>
                );

            case "items":
                return (
                    <div className="relative flex items-end gap-2">

                        {order.orderItems.map(item => (
                            <>[{item.item.itemName}]</>
                        )) }

                    </div>
                );
            case "category":
                return (
                    <div className="relative flex items-end gap-2">

                        {order.orderItems.map(item => (
                            <>{item.item.itemCategory} </>
                        )) }

                    </div>
                );

            case "total":
                return (
                    <div>{order.total + " $"}</div>
                );
            default:
                return cellValue;
        }
    }, [orders]);

    return (
        <>
            <div className={"mx-3 pb-3 flex items-center justify-between"}>
                <h2>Orders</h2>
            </div>
            {orders ? <>


                <Table aria-label="Example table with custom cells" className={"min-h-[400px]"}>

                    <TableHeader columns={columns}>
                        {(column) => (
                            <TableColumn key={column.uid} align={column.uid === "actions" ? "center" : "start"}>
                                {column.name}
                            </TableColumn>
                        )}
                    </TableHeader>
                    <TableBody items={orders || []}>
                        {(item) => (
                            <TableRow key={crypto.randomUUID()}>
                                {(columnKey) => <TableCell>{renderCell(item, columnKey)}</TableCell>}
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </> : <div className={"flex flex-col items-center justify-center w-full h-[400px]"}><Loader/></div>}
            <ProductPagination/>
        </>
    );
}

export default Orders
