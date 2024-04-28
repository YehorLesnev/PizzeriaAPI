"use client"
import React, {ChangeEvent, useEffect, useState} from "react";
import {
    Modal,
    ModalContent,
    ModalHeader,
    ModalBody,
    ModalFooter,
    Button,
    useDisclosure,
    Input,
    Tooltip, Checkbox
} from "@nextui-org/react";
import {EditIcon} from "@nextui-org/shared-icons";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {postAddress, postItem, postOrder, postRecipe, updateItem} from "@/store/services/customerService";
import {CgAdd} from "react-icons/cg";
import {getProducts} from "@/store/services/productService";
import {BiShoppingBag} from "react-icons/bi";
import {DecodedToken} from "@/components/navbar/navbar";
import {FaFirstOrder} from "react-icons/fa6";
import {BsBag} from "react-icons/bs";

interface OrderModalProps {
    ItemId: string
    ItemName: string
    ItemPrice: number
}

export default function OrderModal({ItemId, ItemName, ItemPrice}: OrderModalProps) {
    const dispatch = useAppDispatch()
    const {user} = useAppSelector(state => state.user)

    const {isOpen, onOpen, onOpenChange} = useDisclosure();
    const [quantity, setQuantity] = useState<number>(0)
    const [customerId, setCustomerId] = useState<string| null>(null)
    const [totalPrice, setTotalPrice] = useState<number>(0)
    const [isDelivery, setIsDelivery] = useState<boolean>(false)
    const [address, setAddress] = useState<string>("")
    const [city, setCity] = useState<string>("")
    const [deliveryAddressId, setDeliveryAddressId] = useState<string | null>(null)
    useEffect(() => {
        if (user) {
            setCustomerId(user[DecodedToken.CustomerId])
        }
    }, [user]);

    useEffect(() => {
        setTotalPrice( quantity * ItemPrice)
    }, [quantity, ItemPrice]);
    const createOrder = async () => {
        if (quantity){
            const date = new Date().toISOString()

            const res = await postOrder(date, customerId, ItemId, quantity, isDelivery, deliveryAddressId)
        }

    }

    const createAddress = async () => {
        const res = await postAddress(address, city)
        setDeliveryAddressId(res)
    }


    // @ts-ignore
    return (
        <>
                    <div className={"flex mt-2"} >
                        <Button onClick={onOpen}
                            className={
                                "bg-gray-100 text-black hover:bg-gray-200 transition duration-300 flex items-center justify-center flex-grow"
                            }
                        >
                            Buy <BiShoppingBag/>
                        </Button>
                    </div>


            <Modal
                isOpen={isOpen}
                onOpenChange={onOpenChange}
                placement="top-center"
            >
                <ModalContent>
                    {(onClose) => (
                        <>
                            <ModalHeader className="flex flex-col gap-1">Are you sure to buy a {ItemName} ? </ModalHeader>
                            <ModalBody className={"py-10"}>


                            <BiShoppingBag className={"m-auto py-3"} size={120} color={"gray"} />
                            <div className={"text-center"}>Choose quantity of item</div>
                            <div className={"p-3 m-3 flex items-center justify-between"}>

                                <Button onClick={() => setQuantity(prevState => prevState  && prevState - 1)}>-</Button>
                                {quantity}
                                <Button onClick={() => setQuantity(prevState => prevState + 1)}>+</Button>

                            </div>
                                <div className={"p-3 m-3 space-y-3"}>
                                <div >Total price: {totalPrice} $</div>
                                <div ><Checkbox isSelected={isDelivery} onValueChange={setIsDelivery} /> Delivery </div>
                                    {isDelivery &&<>
                                        <Input  autoFocus
                                                label="City"
                                                placeholder="Enter your city"
                                                variant="bordered" value={city} onValueChange={setCity} />
                                        <Input  autoFocus
                                                label="Address"
                                                placeholder="Enter your address"
                                                variant="bordered" value={address} onValueChange={setAddress} />
                                    {deliveryAddressId ? <div className={"text-success"}>Adress successfuly confirmed</div> : <Button isDisabled={!address && !city} color={"success"} className={"text-white"} onClick={createAddress}>Confirm address</Button>}


                                    </>

                                    }

                                </div>
                            </ModalBody>
                            <ModalFooter>
                                <Button color="danger" variant="flat" onPress={onClose}>
                                    Close
                                </Button>
                                <Button color="secondary" onPress={() => {
                                    onClose()
                                    createOrder()
                                }}>
                                    Create order
                                </Button>
                            </ModalFooter>
                        </>
                    )}
                </ModalContent>
            </Modal>
        </>
    )
}
