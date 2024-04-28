"use client"
import React, {PureComponent, useEffect, useMemo, useState} from 'react';
import {
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    ResponsiveContainer,
    Area,
    AreaChart
} from 'recharts';
import {getStaffPayroll, getStatistics, IStatisticParams} from "@/store/services/statisticService";
import {Button, CalendarDate, DateInput, Select, SelectItem, TimeInput} from "@nextui-org/react";
import {DateValue, parseDate} from "@internationalized/date";
import {CalendarBoldIcon} from "@nextui-org/shared-icons";
import {formatDate, formatTime} from "@/lib/utils";
import {Selection} from "@react-types/shared";
import DownloadSection from "@/components/tabs/manager-tabs/statistics/charts/download-section";
import {
    Table,
    TableHeader,
    TableColumn,
    TableBody,
    TableRow,
    TableCell
} from "@nextui-org/react";
import {Loader} from "@/components/loader/loader";
import { string } from 'zod';

interface Column {
    name: string,
    uid: string
}

const columns: Column[] = [
    {name: "POSITION", uid: "position"},
    {name: "FIRST NAME", uid: "firstName"},
    {name: "LAST NAME", uid: "lastName"},
    {name: "NUMBER OF ORDERS", uid: "numberOfOrders"},
    {name: "ORDERS TOTAL SUM", uid: "ordersTotalSum"},
    {name: "AVERAGE ORDER TOTAL", uid: "averageOrderTotal"},
];

interface IEntities {
    id: number,
    title: string
}

interface IStaff {
    firstName: string
    lastName: string
    position: string
}

export const entities = [{id: 1, title: 'Days'},
    {id: 2, title: 'Months'},
    {id: 3, title: 'Years'}]

    
    interface IItem {
        staff: IStaff,
        numberOfOrders: number
        ordersTotalSum: number
        averageOrderTotal: number
    }

const StatisticChart6 = () => {
    const [data, setData] = useState(null)
    const [startDate, setStartDate] = useState<DateValue>(parseDate("2024-01-01"));
    const [selectedEntity, setSelectedEntity] = React.useState<Selection>(new Set(["Days"]));

    const renderCell = React.useCallback((item: IItem, columnKey: any) => {
        const cellValue: any = item[columnKey];
        switch (columnKey) {
            case "position":
                return (
                    <div className="relative flex items-end gap-2">
                        {item.staff.position}
                    </div>
                );
            case "firstName":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{item.staff.firstName}</p>
                    </div>
                );
                case "lastName":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{item.staff.firstName}</p>
                    </div>
                );
            case "numberOfOrders":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{item.numberOfOrders}</p>
                    </div>
                );

            case "ordersTotalSum":
                return (
                    <div className="relative flex items-end gap-2">
                        {item.ordersTotalSum}
                    </div>
                );
            case "averageOrderTotal":
                return (
                    <div className="relative flex items-end gap-2">
                        {item.averageOrderTotal}
                    </div>
                );
            default:
                return cellValue;
        }
    }, [data]);

    const selectedValue = useMemo(
        () => Array.from(selectedEntity).join(", ").replaceAll("_", " "),
        [selectedEntity],
    );
    const getSale = async ({dateStart, dateEnd}: any) => {
        const formattedStart = formatDate(dateStart)
        getStatistics({date: formattedStart, uri: 'StaffOrdersInfo', entity: selectedValue}).then((res) => {
            setData(res)
        } )
    }
    useEffect(() => {
        const formattedStart = formatDate(startDate)
        getStatistics({date: formattedStart , uri: 'StaffOrdersInfo', entity: selectedValue}).then((res) => {
            setData(res)
        })
    }, [selectedValue]);

    return<div className={"flex flex-col items-center py-28"}>
        <div className={"flex items-center justify-center gap-3 p-2 w-full"}>
            <h1 className={"font-bold text-2xl p-3"}>Staff Orders Info</h1>
            <Select
                label="Select an interval"
                className="max-w-xs"
                variant={"faded"}
                selectedKeys={selectedEntity}
                onSelectionChange={setSelectedEntity}
            >
                {entities.map((entity) => (
                    <SelectItem key={entity.title} value={entity.title}>
                        {entity.title}
                    </SelectItem>
                ))}
            </Select>
        </div>


        <div className={"flex justify-between h-96 w-[125vh] bg-gray-50 rounded-2xl p-4 m-2"}>
        {data ? <>
                <Table aria-label="Example table with custom cells" className={"min-h-[400px]"}>
                    <TableHeader columns={columns}>
                        {(column) => (
                            <TableColumn key={column.uid} align={column.uid === "actions" ? "center" : "start"}>
                                {column.name}
                            </TableColumn>
                        )}
                    </TableHeader>
                    <TableBody items={data || []}>
                        {(item) => (
                            <TableRow key={crypto.randomUUID()}>
                                {(columnKey) => <TableCell>{renderCell(item, columnKey)}</TableCell>}
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </> : <div className={"flex flex-col items-center justify-center w-full h-[400px]"}><Loader/></div>}
            {//data 
                // <ResponsiveContainer width="100%" height="100%">
                //     <AreaChart
                //         width={500}
                //         height={400}
                //         data={data}
                //         margin={{
                //             top: 10,
                //             right: 30,
                //             left: 0,
                //             bottom: 0,
                //         }}
                //     >

                //         <CartesianGrid strokeDasharray="3 3" />
                //         <XAxis dataKey="date" />
                //         <YAxis />
                //         <Tooltip />
                //         <Area  dataKey="numberOfOrders" stackId="1" stroke="red" fill="red" />
                //         <Area  dataKey="numberOfDelivery" stackId="1" stroke="#e2cc00" fill="#edbb09" />

                //         <Area type="monotone" dataKey="deliveryPercentage"  stackId="1" stroke="blue" fill="blue" />

                //     </AreaChart>
                // </ResponsiveContainer>
            }

        </div>
        <div className={"w-full flex justify-between items-end p-5"}>
            <DateInput
                className={"w-80"}
                label="Date"
                defaultValue={parseDate("2024-01-01")}
                value={startDate}
                onChange={setStartDate}
                labelPlacement="outside"
                endContent={
                    <CalendarBoldIcon className="text-2xl text-default-400 pointer-events-none flex-shrink-0" />
                }
            />
            <Button className={"w-80"} color={"primary"} onClick={() => getSale({date: startDate})}>Apply</Button>


        </div>
        <DownloadSection date={startDate} entity={"StaffOrdersInfo"} />

    </div>
}

export default StatisticChart6