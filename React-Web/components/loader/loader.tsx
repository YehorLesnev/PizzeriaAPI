"use client"
import { tailspin } from 'ldrs'

tailspin.register()




export const Loader = () => {
    return <div className={""}><l-tailspin
        size="40"
        stroke="5"
        speed="0.9"
        color="orange"
    ></l-tailspin> </div>
}