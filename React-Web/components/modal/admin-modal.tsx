"use client"
import React, {useState} from "react";
import {
    Modal,
    ModalContent,
    ModalHeader,
    ModalBody,
    ModalFooter,
    Button,
    useDisclosure,
    Checkbox,
    Input,
    Link,
    Tooltip
} from "@nextui-org/react";
import {EditIcon} from "@nextui-org/shared-icons";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {getCustomers, postCustomer, updateCustomer} from "@/store/services/customerService";
import {CgAdd} from "react-icons/cg";
import {getStaff, postStaff, updateStaff} from "@/store/services/staffService";
import {is} from "immer/src/utils/common";

interface IEditModalProps {
    customerId?: string
    firstName?: string
    lastName?: string
    phoneNumber?: string
    isPost?: boolean
    entity: string
    hourlyRate?: string,
    position?: string
}

export default function AdminModal({customerId, firstName, lastName, phoneNumber, isPost, hourlyRate, position, entity}: IEditModalProps) {
    const {isOpen, onOpen, onOpenChange} = useDisclosure();
    const [name, setName] = useState<string>(firstName || '')
    const [rate, setRate] = useState<string>(hourlyRate || '')
    const [statePosition, setStatePosition] = useState<string>(position || '')
    const [last, setLast] = useState<string>(lastName || '')
    const [phone, setPhone] = useState<string>(phoneNumber || '')
    const [userEmail, setUserEmail] = useState<string>( '')
    const [password, setPassword] = useState<string>('')
    const dispatch = useAppDispatch()
    const {activePage, limit} = useAppSelector(state => state.product)

    console.log(userEmail, 'userEmail')
    const changeCustomer = async () => {
        if(entity === "customer") {
            if (!isPost) {
                updateCustomer(customerId, name, last, phone).then(() => {
                    dispatch(
                        getCustomers({pageNumber: activePage, pageSize: limit}),
                    );
                })
            } else {
                postCustomer(name, last, phone, userEmail, password).then(() => {
                    dispatch(
                        getCustomers({pageNumber: activePage, pageSize: limit})
                    )
                })
            }

        }else {
            changeStaff()
        }
    }

    const changeStaff = async () => {
        if (!isPost) {
            updateStaff(customerId, name, last, phone, rate, statePosition).then(() => {
                dispatch(
                    getStaff({pageNumber: activePage, pageSize: limit}),
                );
            })
        } else {
            postStaff(name, last, phone, rate, statePosition).then(() => {
                dispatch(
                    getStaff({pageNumber: activePage, pageSize: limit})
                )
            })
        }
        }



    // @ts-ignore
    return (
        <>
            {isPost ?
                <Tooltip content="Add user">
                    <span className="text-lg text-default-400 cursor-pointer active:opacity-50" onClick={onOpen}>
                <CgAdd color={"gray"} className={"cursor-pointer"} />
              </span>
                </Tooltip>

                :

                <Tooltip content="Edit user">
              <span className="text-lg text-default-400 cursor-pointer active:opacity-50" onClick={onOpen}>
                <EditIcon/>
              </span>
                </Tooltip>
            }


            <Modal
                isOpen={isOpen}
                onOpenChange={onOpenChange}
                placement="top-center"
            >
                <ModalContent>
                    {(onClose) => (
                        <>
                            <ModalHeader className="flex flex-col gap-1">{isPost ? <div>Add </div> : <div>Update </div>} </ModalHeader>
                            <ModalBody>
                                <Input
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    defaultValue={firstName}
                                    autoFocus
                                    label="First name"
                                    placeholder="Enter user's name"
                                    variant="bordered"

                                />
                                {entity === "staff" && <>

                                    <Input
                                        value={rate}
                                        onChange={(e) => setRate(e.target.value)}
                                        defaultValue={String(hourlyRate)}
                                        label="Hourly rate"
                                        placeholder="Enter user's hourly rate"
                                        variant="bordered"
                                    />

                                    <Input
                                        value={statePosition}
                                        onChange={(e) => setStatePosition(e.target.value)}
                                        defaultValue={position}
                                        label="Position"
                                        placeholder="Enter user's position"
                                        variant="bordered"
                                    />

                                </>
                                }


                                <Input
                                    value={last}
                                    onChange={(e) => setLast(e.target.value)}
                                    defaultValue={lastName}
                                    label="Last name"
                                    placeholder="Enter user's last name"
                                    variant="bordered"
                                />
                                {isPost && entity === "customer" &&  <Input
                                    value={userEmail}
                                    onChange={(e) => setUserEmail(e.target.value)}
                                    defaultValue={userEmail}
                                    label="Email"
                                    placeholder="Enter user's email"
                                    variant="bordered"
                                /> }
                                <Input
                                    value={phone}
                                    onChange={(e) => setPhone(e.target.value)}
                                    defaultValue={phoneNumber}
                                    label="Phone number"
                                    placeholder="Enter user's number"
                                    variant="bordered"
                                />
                                {isPost && entity === "customer" &&  <Input
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    defaultValue={password}
                                    label="Password"
                                    placeholder="Enter user's password"
                                    variant="bordered"
                                /> }
                            </ModalBody>
                            <ModalFooter>
                                <Button color="danger" variant="flat" onPress={onClose}>
                                    Close
                                </Button>
                                <Button color="secondary" onPress={() => {
                                    onClose()
                                    changeCustomer()
                                }}>
                                    Save changes
                                </Button>
                            </ModalFooter>
                        </>
                    )}
                </ModalContent>
            </Modal>
        </>
    )
}
