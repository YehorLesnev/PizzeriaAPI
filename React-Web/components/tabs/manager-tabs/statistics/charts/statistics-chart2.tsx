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
import {Button, CalendarDate, DateInput, Input, Select, SelectItem, TimeInput} from "@nextui-org/react";
import {DateValue, parseDate} from "@internationalized/date";
import {CalendarBoldIcon} from "@nextui-org/shared-icons";
import {formatDate, formatTime} from "@/lib/utils";
import {Selection} from "@react-types/shared";


interface IEntities {
    id: number,
    title: string
}
export const entities = [{id: 1, title: 'Days'},
    {id: 2, title: 'Months'},
    {id: 3, title: 'Years'}]

const StatisticChart2 = () => {
    const [data, setData] = useState(null)
    const [startDate, setStartDate] = useState<DateValue>(parseDate("2024-01-01"));
    const [endDate, setEndDate] = useState<DateValue>(parseDate("2024-01-15"))
    const [selectedEntity, setSelectedEntity] = useState<Selection>(new Set(["Days"]));
    const [category, setCategory] = useState<string>("Pizza")
    const selectedValue = useMemo(
        () => Array.from(selectedEntity).join(", ").replaceAll("_", " "),
        [selectedEntity],
    );
    const getSale = async ({dateStart, dateEnd}: any) => {
        const formattedStart = formatDate(dateStart)
        const formattedEnd = formatDate(dateEnd)
        // @ts-ignore
        getStatistics({dateStart: formattedStart, dateEnd: formattedEnd, uri: 'Sales', entity: selectedValue, itemCategory: category}).then((res) => {
            setData(res)
        } )
    }
    useEffect(() => {
        const formattedStart = formatDate(startDate)
        const formattedEnd = formatDate(endDate)
        // @ts-ignore
        getStatistics({dateStart: formattedStart,  dateEnd: formattedEnd , uri: 'Sales', entity: selectedValue, itemCategory: category}).then((res) => {
            setData(res)
        })
    }, [selectedValue]);

    return<div className={"flex flex-col items-center py-28"}>
        <div className={"flex items-center justify-center gap-3 p-2 w-full"}>
            <h1 className={"font-bold text-2xl p-3"}>Sales</h1>
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

            {data &&
                <ResponsiveContainer width="100%" height="100%">
                    <AreaChart
                        width={500}
                        height={400}
                        data={data}
                        margin={{
                            top: 10,
                            right: 30,
                            left: 0,
                            bottom: 0,
                        }}
                    >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="salesDate" tickFormatter={(value) => value.substring(0, 10)} />
                        <YAxis />
                        <Tooltip />
                        <Area  dataKey="totalRevenue" stackId="1" stroke="#e2cc00" fill="#edbb09" />
                    </AreaChart>
                </ResponsiveContainer>
            }

        </div>
        <div className={"w-full flex justify-between items-end p-5"}>
            <DateInput
                className={"w-80"}
                label="Start date"
                defaultValue={parseDate("2024-01-01")}
                value={startDate}
                onChange={setStartDate}
                labelPlacement="outside"
                endContent={
                    <CalendarBoldIcon className="text-2xl text-default-400 pointer-events-none flex-shrink-0" />
                }
            />
            <Button className={"w-80"} color={"primary"} onClick={() => getSale({dateStart: startDate, dateEnd: endDate})}>Apply</Button>
            <DateInput
                className={"w-80"}
                value={endDate}
                onChange={setEndDate}
                label="End date"
                defaultValue={parseDate("2024-01-01")}
                labelPlacement="outside"
                endContent={
                    <CalendarBoldIcon className="text-2xl text-default-400 pointer-events-none flex-shrink-0" />
                }
            />

            <Input onValueChange={setCategory} value={category} />

        </div>
    </div>
}

export default StatisticChart2