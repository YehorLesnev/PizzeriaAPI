import clsx, {ClassValue} from "clsx";
import {twMerge} from "tailwind-merge";
import {TimeValue} from "@react-types/datepicker";
import {DateValue} from "@internationalized/date";

export function cn(...inputs: ClassValue[]) {
    return twMerge(clsx(inputs));
}


export function formatTime(time: TimeValue) {
    if (time) {
        const { hour, minute, second } = time;
        const formattedHour = String(hour).padStart(2, '0');
        const formattedMinute = String(minute).padStart(2, '0');
        const formattedSecond = String(second).padStart(2, '0');
        return `${formattedHour}:${formattedMinute}:${formattedSecond}`;
    }

}

export function formatDate(date: DateValue) {
    if (date) {
        const { day, month, year } = date;

        return `${year}-${month}-${day}`;
    }

}