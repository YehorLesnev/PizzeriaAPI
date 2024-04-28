"use client"
import { z } from "zod";
import {useForm, SubmitHandler} from "react-hook-form";
import { register } from "@/store/services/authService";
import { useEffect } from "react";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {useRouter} from "next/navigation";
import {zodResolver} from "@hookform/resolvers/zod";
import {Button} from "@nextui-org/button";
import {Link} from "@nextui-org/link";
import {Image} from "@nextui-org/image";
import {signUpSchema} from "@/types/validation";
import {Input} from "@nextui-org/input";
import Logo from "@/assets/Logo";
import {clearError} from "@/store/features/auth/authSlice";

const Register = () => {
    const router = useRouter()
    const dispatch = useAppDispatch();
    const {error} = useAppSelector(state => state.user)
    useEffect(() => {
        dispatch(clearError());
    }, [dispatch]);
    const {
        register: reg,
        handleSubmit,
        formState: { errors },
    } = useForm<z.infer<typeof signUpSchema>>()
    const onSubmit: SubmitHandler<z.infer<typeof signUpSchema>> = async (values: z.infer<typeof signUpSchema>) => {
        const res = await dispatch(register(values));
        if (res.payload === 201) {
            router.push("/auth/login");
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
                            Create an account
                        </h1>
                    </div>

                    <form onSubmit={handleSubmit(onSubmit)} className={"flex flex-col items-center gap-5 "}>
                        <Input color={"primary"} variant={"bordered"} placeholder="Name" {...reg("firstName", {required: true})} />
                        <Input color={"primary"} variant={"bordered"} placeholder="Last name" {...reg("lastName", {required: true})} />
                        <Input color={"primary"} variant={"bordered"} placeholder="Phone number" {...reg("phoneNumber", {required: true})} />
                        <Input color={"primary"} variant={"bordered"} placeholder="Example@gmail.com" {...reg("email", {required: true})} />




                        <Input type={"password"} color={"primary"} variant={"bordered"} placeholder="Password" {...reg("password", { required: true })} />

                        <Button type="submit" variant={"solid"} color={"primary"} className={"w-full py-7"}>Register</Button>
                        {error && <div className={"text-red-500"}>{error}</div> }
                        <span>Already have an account ? <Link href={"/auth/login"}>Login</Link></span>
                    </form>
                </div>
            </div>

        </div>
    );
};

export default Register;
