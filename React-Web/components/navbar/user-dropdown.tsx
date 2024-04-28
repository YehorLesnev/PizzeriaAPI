"use client"
import {Dropdown, DropdownItem, DropdownMenu, DropdownTrigger} from "@nextui-org/dropdown";
import {Avatar} from "@nextui-org/avatar";
import {Chip} from "@nextui-org/chip";
import Cookies from "js-cookie";
import {useRouter} from "next/navigation";
import {Link} from "@nextui-org/react";

interface IUserDropdown {
    role: string,
    email: string
}
export const UserDropdown = ({role, email}: IUserDropdown) => {
const router = useRouter()
    const logout = () => {
        Cookies.remove("jwt")
        router.push("/auth/login")
    }
    return (<>
        <Dropdown placement="bottom-end">
            <DropdownTrigger>
                <Avatar
                    isBordered
                    as="button"
                    className="transition-transform"
                    color="primary"
                    size="sm"
                />
            </DropdownTrigger>
            <DropdownMenu aria-label="Profile Actions" variant="flat">
                <DropdownItem key="profile" className="h-14 gap-2">
                    <p className="font-semibold">Signed in as</p>
                    <p className="font-semibold">{email}</p>
                </DropdownItem>
                <DropdownItem key="role" className={"flex justify-between w-full"}>Role: <Chip color={"secondary"}> {role} </Chip> </DropdownItem>
                {role?.toLowerCase() !== "customer" && (
                    <DropdownItem key={role}>
                        <Link className="text-black w-full" href={'/' + role.toLowerCase()}>
                            {role} page
                        </Link>
                    </DropdownItem>
                )}
                <DropdownItem key="role"><Link className={"text-black w-full"} href={"/orders"}>Orders</Link></DropdownItem>


                <DropdownItem key="logout" color="danger" onClick={logout}>
                    Log Out
                </DropdownItem>
            </DropdownMenu>
        </Dropdown>


    </>)
}