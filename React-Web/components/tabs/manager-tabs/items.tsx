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
    Button
} from "@nextui-org/react";
import {DeleteIcon, EditIcon, EyeIcon} from "@nextui-org/shared-icons";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {IProduct, setActivePage, setTotalCount} from "@/store/features/product/productSlice";
import {deleteCustomer, deleteItem, getCustomerCount} from "@/store/services/customerService";
import ProductPagination from "@/components/pagination/product-pagination";
import {Loader} from "@/components/loader/loader";
import AdminModal from "@/components/modal/admin-modal";
import {getProducts, getProductsCount} from "@/store/services/productService";
import {Dropdown, DropdownItem, DropdownMenu, DropdownTrigger} from "@nextui-org/dropdown";
import ItemsModal from "@/components/modal/items-modal";

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
    {name: "CATEGORY", uid: "category"},
    {name: "SIZE", uid: "size"},
    {name: "PRICE", uid: "price"},
    {name: "ACTIONS", uid: "actions"},
];

export {columns};

const Items = () => {
    const [selectedKeys, setSelectedKeys] = React.useState(new Set(["Pizza"]));

    const selectedValue = React.useMemo(
        () => Array.from(selectedKeys).join(", ").replaceAll("_", " "),
        [selectedKeys]
    );
    const dispatch = useAppDispatch()
    const {products,} = useAppSelector(
        (state) => state.product,
    );
    const {activePage, limit} = useAppSelector(state => state.product)

    useEffect(() => {
        const getProductsCounter = async () => {
            const res =  getProductsCount({pageNumber: null, pageSize: null, itemCategory: selectedValue})
            res.then(res => {
                let count = 0
                res.map(() => count++)

                dispatch(setTotalCount(count))
            })
        }
        getProductsCounter()
        dispatch(setActivePage(1))
    }, [selectedValue]);
    useEffect(() => {
        dispatch(
            getProducts({pageNumber: activePage, pageSize: limit, itemCategory: selectedValue}),
        );
    }, [dispatch, activePage, limit, selectedValue]);


    const renderCell = React.useCallback((item: IProduct, columnKey: any) => {
        switch (columnKey) {
            case "name":
                return (
                    <div>{item.itemName}</div>
                );
            case "category":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{item.itemCategory}</p>
                    </div>
                );
            case "size":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{item.itemSize}</p>
                    </div>
                )
            case "price":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{item.itemPrice}</p>
                    </div>
                )
            case "actions":
                return (
                    <div className="relative flex items-end gap-2">

                        <ItemsModal ItemName={item.itemName} ItemCategory={item.itemCategory} ItemSize={item.itemSize} ItemPrice={String(item.itemPrice)} ItemId={item.itemId} RecipeId={item.recipeId} ImagePath={item.imagePath} />
                        <Tooltip color="danger" content="Delete user">
              <span className="text-lg text-danger cursor-pointer active:opacity-50" >
                <DeleteIcon  onClick={() => deleteItem(item.itemId).then(() => {
                    dispatch(
                        getProducts({pageNumber: activePage, pageSize: limit, itemCategory: selectedValue}),
                    );
                })}/>
              </span>
                        </Tooltip>
                    </div>
                );
            default:
                return <div>default</div>;
        }
    }, [products]);

    return (
        <>
            <div className={"mx-3 pb-3 flex items-center justify-between"}>
                <h2>Items</h2>
                <Dropdown>
                    <DropdownTrigger>
                        <Button
                            variant="bordered"
                            color={"primary"}
                            className="capitalize"
                        >
                            {selectedValue}
                        </Button>
                    </DropdownTrigger>
                    <DropdownMenu
                        aria-label="Single selection example"
                        variant="flat"
                        disallowEmptySelection
                        selectionMode="single"
                        selectedKeys={selectedKeys}
                        onSelectionChange={setSelectedKeys}
                    >
                        <DropdownItem key="Pizza">Pizza</DropdownItem>
                        <DropdownItem key="Fries">Fries</DropdownItem>
                        <DropdownItem key="Burger">Burgers</DropdownItem>
                        <DropdownItem key="HotDog">HotDogs</DropdownItem>
                    </DropdownMenu>
                </Dropdown>
                <ItemsModal isPost={true} />

            </div>
            {products ? <>


                <Table aria-label="Example table with custom cells" className={"min-h-[400px]"}>

                    <TableHeader columns={columns}>
                        {(column) => (
                            <TableColumn key={column.uid} align={column.uid === "actions" ? "center" : "start"}>
                                {column.name}
                            </TableColumn>
                        )}
                    </TableHeader>
                    <TableBody items={products || []}>
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

export default Items
