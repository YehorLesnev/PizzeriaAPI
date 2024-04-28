"use client"
import { Link } from "@nextui-org/link";
import { Snippet } from "@nextui-org/snippet";
import { Code } from "@nextui-org/code"
import { button as buttonStyles } from "@nextui-org/theme";
import { siteConfig } from "@/config/site";
import { title, subtitle } from "@/components/primitives";
import { GithubIcon } from "@/components/icons";
import {Image} from "@nextui-org/image";
import {landingItems} from "@/lib/constants";
import {Card, CardBody, CardHeader} from "@nextui-org/card";
import {MacbookScroll} from "@/components/ui/macbook-scroll";
import {CiLogin} from "react-icons/ci";
import Footer from "@/components/footer/footer";

export default function Home() {
	return (
		<>
		<section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
			<div className="inline-block max-w-lg text-center justify-center mt-20">
				<h1 className={title()}>Choose your&nbsp;</h1>
				<h1 className={title({ color: "yellow" })}>favourite&nbsp;</h1>
				<br />
				<h1 className={title()}>
					dish regardless of your taste experience.
				</h1>
				<h2 className={subtitle({ class: "mt-4" })}>
					Juicy, high quality and yummy Pizzeria in your town!
				</h2>
			</div>

			<div className="flex gap-3">
				<Link
					href={"/auth/register"}
					className={buttonStyles({ color: "primary", radius: "full", variant: "shadow" })}
				>
					Discover
				</Link>
				<Link
					className={buttonStyles({ variant: "bordered", radius: "full" })}
					href={"/auth/login"}
				>
					<CiLogin/>
					Log in
				</Link>
			</div>


			<div className="flex items-center m-5">
				{landingItems && landingItems.map(item => (
					<Link href={`/${item.title.toLowerCase()}`} key={item.id} className={"m-4"}>
					<Card className="py-4">
						<CardHeader className="pb-0 pt-2 px-4 flex-col items-start">
							<h4 className="font-bold text-large">{item.title}</h4>
						</CardHeader>
						<CardBody className="overflow-visible py-2">
							<Image
								isZoomed
								alt="Card background"
								className="object-cover rounded-xl"
								src={item.img}
								width={470}
								height={470}
							/>
						</CardBody>
					</Card>
					</Link>
				))}

			</div>
			<MacbookScroll src={'https://w.wallhaven.cc/full/o5/wallhaven-o5xdem.png'} />
		</section>
			<Footer/>

		</>
	);
}
