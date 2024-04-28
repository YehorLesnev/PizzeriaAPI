"use client"
import { z } from "zod";
import {useForm, SubmitHandler} from "react-hook-form";
import {login, register} from "@/store/services/authService";
import { useEffect } from "react";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {useRouter} from "next/navigation";
import {zodResolver} from "@hookform/resolvers/zod";
import {Button} from "@nextui-org/button";
import {Link} from "@nextui-org/link";
import {Image} from "@nextui-org/image";
import {signInSchema} from "@/types/validation";
import {Input} from "@nextui-org/input";
import Logo from "@/assets/Logo";
import {clearError} from "@/store/features/auth/authSlice";
import Cookies from "js-cookie";

const Login = () => {
    const router = useRouter()
    const dispatch = useAppDispatch();
    const {error} = useAppSelector(state => state.user)
    useEffect(() => {
        dispatch(clearError());

    }, [dispatch]);
    useEffect(() => {
        Cookies.remove("jwt")
    }, []);
    const {
        register: reg,
        handleSubmit,
        formState: { errors },
    } = useForm<z.infer<typeof signInSchema>>()
    const onSubmit: SubmitHandler<z.infer<typeof signInSchema>> = async (values: z.infer<typeof signInSchema>) => {
        const res = await dispatch(login(values));
        // @ts-ignore
        if (res.payload.status === 200) {
            router.push("/pizza");
        }
    };

    return (
        <div
            className={
                "md:flex justify-between h-screen w-full max-w-screen-2xl m-auto"
            }
        >
            <div
                className={
                    "flex flex-col w-full md:w-[50%] m-auto h-full items-center p-6 md:p-14 xl:p-32 rounded-2xl drop-shadow-2xl"
                }
            >
                <div className={"w-full flex flex-col justify-around h-full"}>
                    <div className={"flex flex-col justify-center items-center"}>
                        <Logo size={80}/>

                        <h1
                            className={
                                "text-2xl sm:text-3xl md:text-4xl font-bold text-zinc-700 text-center"
                            }
                        >
                            Log In
                        </h1>
                    </div>

                    <form onSubmit={handleSubmit(onSubmit)} className={"flex flex-col items-center gap-5 "}>
                        <Input color={"primary"} variant={"bordered"} placeholder="example@gmail.com" {...reg("email", {required: true})} />


                        <Input type={"password"} color={"primary"} variant={"bordered"} placeholder="password" {...reg("password", { required: true })} />

                        <Button type="submit" variant={"solid"} color={"primary"} className={"w-full py-7"}>Log In</Button>
                        {error && <div className={"text-red-500"}>{error}</div> }
                        <span>Don&apos;t have an account ? <Link href={"/auth/register"}>Register</Link></span>
                    </form>
                </div>
            </div>

        </div>
    );
};

export default Login;
