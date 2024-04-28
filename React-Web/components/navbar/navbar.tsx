"use client"
import {
	Navbar as NextUINavbar,
	NavbarContent,
	NavbarMenu,
	NavbarBrand,
	NavbarItem,
	NavbarMenuItem,
} from "@nextui-org/navbar";
import { Link } from "@nextui-org/link";
import { siteConfig } from "@/config/site";
import NextLink from "next/link";
import Logo from "@/assets/Logo";
import {useEffect, useState} from "react";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import Cookies from "js-cookie";
import {useRouter} from "next/navigation";
import {UserDropdown} from "@/components/navbar/user-dropdown";
import {Button} from "@nextui-org/button";


export const enum DecodedToken {
	Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
	Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
	CustomerId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
}

export const Navbar = () => {
	const router = useRouter()
	const [token, setToken] = useState(null);
	const {user} = useAppSelector(state => state.user)
	const [role, setRole] = useState<string>("")
	const [email, setEmail] = useState<string>("")
	console.log(user, "user!")
	useEffect(() => {
		if (user) {
			setRole(user[DecodedToken.Role])
			setEmail(user[DecodedToken.Email])
		}

	}, [user]);

	useEffect(() => {
		const tokenFromCookies = Cookies.get("jwt");
		// @ts-ignore
		setToken(tokenFromCookies);
	}, []);

	return (
		<NextUINavbar maxWidth="xl" position="sticky" className={"border-b"}>
			<NavbarContent className="basis-1/5 sm:basis-full" justify="start">
				<NavbarBrand as="li" className="gap-3 max-w-fit">
					<NextLink className="flex justify-start items-center gap-1" href="/">
						<Logo isBordered={true}/>
						<p className="font-black text-inherit text-primary text-xl">PizzaShon</p>
					</NextLink>
				</NavbarBrand>
			</NavbarContent>



			<NavbarMenu>
				<div className="mx-4 mt-2 flex flex-col gap-2">
					{siteConfig.navMenuItems.map((item, index) => (
						<NavbarMenuItem key={`${item}-${index}`}>
							<Link
								color={
									index === 2
										? "primary"
										: index === siteConfig.navMenuItems.length - 1
										? "danger"
										: "foreground"
								}
								href="#"
								size="lg"
							>
								{item.label}
							</Link>
						</NavbarMenuItem>
					))}
				</div>
			</NavbarMenu>
			<NavbarContent className="hidden sm:flex gap-4" justify="center">
				<NavbarItem>
					<Link color="foreground" href="/pizza">
						Pizza
					</Link>
				</NavbarItem>
				<NavbarItem>
					<Link href="/fries" color="foreground">
						Fries
					</Link>
				</NavbarItem>
				<NavbarItem>
					<Link href="/burgers" color="foreground" >
						Burgers
					</Link>
				</NavbarItem>
				<NavbarItem>
					<Link href="/hotdogs" color="foreground">
						Hotdogs
					</Link>
				</NavbarItem>
			</NavbarContent>
			<NavbarContent as="div" justify="end">
				{token ? <UserDropdown role={role} email={email} /> : <div className={"flex justify-between items-center px-4 gap-2"}> <Button variant={"bordered"} onClick={() => router.push("/auth/login")}>Log in</Button>  <Button variant={"solid"} onClick={() => router.push("/auth/register")} color={"primary"}>Register</Button> </div>}
			</NavbarContent>
		</NextUINavbar>
	);
};
