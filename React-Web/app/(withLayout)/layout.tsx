"use client"
import {Navbar} from "@/components/navbar/navbar";
import {PropsWithChildren} from "react";


export default function Layout({children}: PropsWithChildren){
    return <>
        <Navbar />
        <main className="px-3 mx-auto min-w-[140vh] pt-16 flex-grow">
            {children}
        </main>
    </>
}