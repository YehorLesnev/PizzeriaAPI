"use client"
import React, {useEffect} from "react";
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
import {DeleteIcon, EditIcon, EyeIcon} from "@nextui-org/shared-icons";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {setActivePage, setTotalCount} from "@/store/features/product/productSlice";
import {deleteCustomer, getCustomerCount, getCustomers} from "@/store/services/customerService";
import ProductPagination from "@/components/pagination/product-pagination";
import {Loader} from "@/components/loader/loader";
import AdminModal from "@/components/modal/admin-modal";

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
    {name: "NAME", uid: "name"},
    {name: "LASTNAME", uid: "lastName"},
    {name: "PHONE", uid: "phone"},
    {name: "EMAIL", uid: "email"},
    {name: "ACTIONS", uid: "actions"},
];



export {columns};

const Customers = () => {
    const dispatch = useAppDispatch()
    const {customers,} = useAppSelector(
        (state) => state.customer,
    );
    const {activePage, limit} = useAppSelector(state => state.product)

    useEffect(() => {
        const getCustomerCounter = async () => {
            const res =  getCustomerCount({pageNumber: null, pageSize: null})
            res.then(res => { console.log(res)
                let count = 0
                res.map(() => count++)

                dispatch(setTotalCount(count))
            })
        }
        getCustomerCounter()
        dispatch(setActivePage(1))

    }, []);

    useEffect(() => {
        dispatch(
            getCustomers({pageNumber: activePage, pageSize: limit}),
        );
    }, [dispatch, activePage, limit]);


    console.log(customers,'6666')
    const renderCell = React.useCallback((user: any, columnKey: any) => {
        const cellValue = user[columnKey];
        switch (columnKey) {
            case "name":
                return (
                  <div>{user.firstName}</div>
                );
            case "lastName":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{user.lastName}</p>
                    </div>
                );
            case "email":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{user.email}</p>
                    </div>
                );
            case "phone":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{user.phoneNumber}</p>
                    </div>
                )
            case "actions":
                return (
                    <div className="relative flex items-end gap-2">

                      <AdminModal entity={"customer"} customerId={user.customerId} firstName={user.firstName} lastName={user.lastName} phoneNumber={user.phoneNumber}  />
                        <Tooltip color="danger" content="Delete user">
              <span className="text-lg text-danger cursor-pointer active:opacity-50" >
                <DeleteIcon  onClick={() => deleteCustomer(user.customerId).then(() => {
                    dispatch(
                        getCustomers({pageNumber: activePage, pageSize: limit}),
                    );
                })}/>
              </span>
                        </Tooltip>
                    </div>
                );
            default:
                return cellValue;
        }
    }, [customers]);

    return (
        <>
            <div className={"mx-3 pb-3 flex items-center justify-between"}>
                <h2>Customers</h2>
                <AdminModal entity={"customer"} isPost={true} />
            </div>
            {customers ? <>


            <Table aria-label="Example table with custom cells" className={"min-h-[400px]"}>

                <TableHeader columns={columns}>
                    {(column) => (
                        <TableColumn key={column.uid} align={column.uid === "actions" ? "center" : "start"}>
                            {column.name}
                        </TableColumn>
                    )}
                </TableHeader>
                <TableBody items={customers || []}>
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

export default Customers
