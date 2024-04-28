"use client"
import React from 'react';
import {Card, CardBody, CardFooter, CardHeader} from "@nextui-org/card";
import {Divider} from "@nextui-org/divider";
import {Link} from "@nextui-org/react";
import {Image} from "@nextui-org/image";
import {InfoIcon} from "@nextui-org/shared-icons";
import Logo from "@/assets/Logo";
import {IoFastFood} from "react-icons/io5";

interface IStatisticCard {
    name: string
    quantity: number
}
const StatisticCard = ({name, quantity}: IStatisticCard) => {
    return (
        <Card className="min-w-[290px]">
            <CardHeader className="flex gap-3">
                <div className={"flex items-center"}>
                    <IoFastFood size={30} color={"orange"} className={"p-1"} />
                    <div className="text-xl flex flex-col">
                        <h1 className="">Total {name}</h1>
                    </div>
                </div>

            </CardHeader>
            <CardBody>
                <h2 className={"text-2xl font-bold"}> +{quantity} last month</h2>
            </CardBody>
            <Divider/>

        </Card>
    );
};

export default StatisticCard;