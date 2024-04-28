"use client"
import {PropsWithChildren, useEffect, useState} from "react";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {DecodedToken} from "@/components/navbar/navbar";
import {useRouter} from "next/navigation";

export default function ManagerLayout({children}: PropsWithChildren){
    const [isEnabled, setIsEnabled] = useState<boolean>(false)
    const router = useRouter()

    const dispatch = useAppDispatch()
    const {user} = useAppSelector(state => state.user)


    if ( !user || user[DecodedToken.Role] !== "Manager"){
        router.push("/")
        return
    } else {
        return <div>{children}</div>

    }
}