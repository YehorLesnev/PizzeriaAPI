"use client"
import React, {useEffect} from "react";
import {Table, TableHeader, TableColumn, TableBody, TableRow, TableCell, User, Chip, Tooltip, getKeyValue} from "@nextui-org/react";
import {DeleteIcon, EditIcon, EyeIcon} from "@nextui-org/shared-icons";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {setActivePage, setTotalCount} from "@/store/features/product/productSlice";
import {Loader} from "@/components/loader/loader";
import AdminModal from "@/components/modal/admin-modal";
import {deleteStaff, getStaff, getStaffCount} from "@/store/services/staffService";
import ProductPagination from "@/components/pagination/product-pagination";

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
    {name: "ROLE", uid: "role"},
    {name: "PHONE", uid: "phone"},
    {name: "RATE", uid: "rate"},
    {name: "POSITION", uid: "position"},
    {name: "ACTIONS", uid: "actions"},
];



export {columns};

const Staff = () => {
    const dispatch = useAppDispatch()
    const {staff,} = useAppSelector(
        (state) => state.staff,
    );
    const {activePage, limit} = useAppSelector(state => state.product)

    useEffect(() => {
        const getStaffCounter = async () => {
            const res =  getStaffCount({pageNumber: null, pageSize: null})
            res.then(res => { console.log(res)
                let count = 0
                res.map(() => count++)

                dispatch(setTotalCount(count))
            })
        }
        getStaffCounter()

        dispatch(setActivePage(1))
    }, []);

    useEffect(() => {
        dispatch(
            getStaff({pageNumber: activePage, pageSize: limit}),
        );
    }, [dispatch, activePage, limit]);


    console.log(staff,'6666')
    const renderCell = React.useCallback((user: any, columnKey: any) => {
        const cellValue = user[columnKey];
        switch (columnKey) {
            case "name":
                return (
                    <div>{user.firstName}</div>
                );
            case "role":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{user.lastName}</p>
                    </div>
                );
            case "phone":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{user.phoneNumber}</p>
                    </div>
                )
            case "position":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{user.position}</p>
                    </div>
                )
            case "rate":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{user.hourlyRate}</p>
                    </div>
                )
            case "actions":
                return (
                    <div className="relative flex items-end gap-2">

                        <AdminModal entity={"staff"} customerId={user.staffId} firstName={user.firstName} lastName={user.lastName} phoneNumber={user.phoneNumber} position={user.position} hourlyRate={user.hourlyRate} />
                        <Tooltip color="danger" content="Delete user">
              <span className="text-lg text-danger cursor-pointer active:opacity-50" >
                <DeleteIcon  onClick={() => deleteStaff(user.staffId).then(() => {
                    dispatch(
                        getStaff({pageNumber: activePage, pageSize: limit}),
                    );
                })}/>
              </span>
                        </Tooltip>
                    </div>
                );
            default:
                return cellValue;
        }
    }, [staff]);

    return (
        <>
            <div className={"mx-3 pb-3 flex items-center justify-between"}>
                <h2>Staff</h2>
                <AdminModal entity={"staff"} isPost={true}   />
            </div>
            {staff ? <>


            <Table aria-label="Example table with custom cells" className={"min-h-[400px]"}>

                <TableHeader columns={columns}>
                    {(column) => (
                        <TableColumn key={column.uid} align={column.uid === "actions" ? "center" : "start"}>
                            {column.name}
                        </TableColumn>
                    )}
                </TableHeader>
                <TableBody items={staff || []}>
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

export default Staff
