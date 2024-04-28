"use client"
import React, {useEffect, useState} from "react";
import {
    Table,
    TableHeader,
    TableColumn,
    TableBody,
    TableRow,
    TableCell,
     DateInput, Button
} from "@nextui-org/react";
import {Loader} from "@/components/loader/loader";
import {FaRegCheckCircle} from "react-icons/fa";
import {GiCancel} from "react-icons/gi";
import {DateValue, parseDate} from "@internationalized/date";
import {formatDate} from "@/lib/utils";
import {downloadXMLPayroll, getDownloads, getStaffPayroll, getStatistics} from "@/store/services/statisticService";
import {CalendarBoldIcon} from "@nextui-org/shared-icons";
import DownloadSection from "@/components/tabs/manager-tabs/statistics/charts/download-section";

const statusColorMap: any = {
    active: "success",
    paused: "danger",
    vacation: "warning",
};

interface Column {
    name: string,
    uid: string
}

const columns: Column[] = [
    {name: "POSITION", uid: "position"},
    {name: "FIRST NAME", uid: "firstName"},
    {name: "LAST NAME", uid: "lastName"},
    {name: "HOURS WORKED", uid: "hoursWorked"},
    {name: "HOURLY RATE", uid: "hourlyRate"},
    {name: "PAYROLL", uid: "payroll"},
];

export interface IDownloadParams {
    dateStart?: any
    dateEnd?: any
    date?: any
    fileType: "PDF" | "XML" | "JSON"
    entity: "StaffPayroll" | "StaffOrdersInfo"
}


export {columns};




interface IItem {
    staffId: string,
    firstName: string,
    lastName: string,
    position: string,
    hoursWorked: number
    hourlyRate: number
    payroll: number
}

const StaticticsChart1 = () => {
    const [data, setData] = useState(null)
    const [startDate, setStartDate] = useState<DateValue>(parseDate("2024-01-01"));
    const [endDate, setEndDate] = useState<DateValue>(parseDate("2024-01-02"))

    const getPayroal = async ({dateStart, dateEnd}: any) => {
        const formattedStart = formatDate(dateStart)
        const formattedEnd = formatDate(dateEnd)
        getStaffPayroll({dateStart: formattedStart, dateEnd: formattedEnd}).then((res) => {
            setData(res)
        })
    }
    useEffect(() => {
        getStaffPayroll({dateStart: "2024-01-01", dateEnd: "2024-01-02"}).then((res) => {
            setData(res)
        })
    }, []);

    const renderCell = React.useCallback((item: IItem, columnKey: any) => {
        const cellValue: any = item[columnKey];
        switch (columnKey) {
            case "position":
                return (
                    <div className="relative flex items-end gap-2">
                        {item.position}
                    </div>
                );
            case "firstName":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{item.firstName}</p>
                    </div>
                );
            case "lastName":
                return (
                    <div className="relative flex items-end gap-2">
                        {item.lastName}
                    </div>
                );
            case "hoursWorked":
                return (
                    <div className="flex flex-col">
                        <p className="text-bold text-sm capitalize text-default-400">{item.hoursWorked}</p>
                    </div>
                );

            case "hourlyRate":
                return (
                    <div className="relative flex items-end gap-2">
                        {item.hourlyRate}
                    </div>
                );
            case "payroll":
                return (
                    <div className="relative flex items-end gap-2">
                        {item.payroll}
                    </div>
                );
            default:
                return cellValue;
        }
    }, [data]);

    return (
        <>
            <div className={"mx-3 pb-3 flex items-center justify-between"}>
                <h2 className={"text-2xl font-bold"}>PayrollStaff</h2>
            </div>
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
            <div className={"w-full flex justify-between items-end p-5"}>
                <DateInput
                    className={"w-80"}
                    label="Start date"
                    defaultValue={parseDate("2024-01-01")}
                    value={startDate}
                    onChange={setStartDate}
                    labelPlacement="outside"
                    endContent={
                        <CalendarBoldIcon className="text-2xl text-default-400 pointer-events-none flex-shrink-0"/>
                    }
                />
                <Button className={"w-80"} color={"primary"}
                        onClick={() => getPayroal({dateStart: startDate, dateEnd: endDate})}>Apply</Button>
                <DateInput
                    className={"w-80"}
                    value={endDate}
                    onChange={setEndDate}
                    label="End date"
                    defaultValue={parseDate("2024-01-01")}
                    labelPlacement="outside"
                    endContent={
                        <CalendarBoldIcon className="text-2xl text-default-400 pointer-events-none flex-shrink-0"/>
                    }
                />
            </div>
            <DownloadSection dateStart={startDate} dateEnd={endDate} entity={"StaffPayroll"} />
        </>
    );
}

export default StaticticsChart1
