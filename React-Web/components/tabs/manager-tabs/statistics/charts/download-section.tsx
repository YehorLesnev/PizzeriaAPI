import {Button} from "@nextui-org/react";
import React from "react";
import {IDownloadParams} from "@/components/tabs/manager-tabs/statistics/charts/statistics-chart1";
import {formatDate} from "@/lib/utils";
import {getDownloads} from "@/store/services/statisticService";

export function downloadXML(xmlData: any, filename: any) {
    const blob = new Blob([xmlData], {type: 'application/xml'});
    const href = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = href;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(href);
}


export function downloadPDF(pdfData: any, filename: any) {
    const blob = new Blob([pdfData], { type: 'application/pdf' });
    const href = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = href;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(href);
}


export function downloadJSON(jsonData: any, filename: any) {
    const blob = new Blob([JSON.stringify(jsonData, null, 2)], { type: 'application/json' });
    const href = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = href;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(href);
}


export const download = async ({dateStart, dateEnd, fileType, entity, date}: IDownloadParams) => {
    const formattedStart = formatDate(dateStart)
    const formattedEnd = formatDate(dateEnd)
    getDownloads({dateStart: formattedStart, dateEnd: formattedEnd, date, entity, fileType}).then((data) => {
        switch (fileType){
            case "XML":
                downloadXML(data, `${entity}.xml`);
                break
            case "PDF":
                downloadPDF(data, `${entity}.pdf`);
                break
            case "JSON":
                downloadJSON(data, `${entity}.json`);
                break

        }

    })
}


const DownloadSection = ({dateEnd, dateStart, entity, date}: Omit<IDownloadParams, "fileType">) => {
    return   <div className={"flex items-center justify-between gap-4"}>
        <Button className={"w-80"} color={"secondary"}
                onClick={() => download({dateStart, dateEnd, date, fileType: "XML", entity})}>Download
            XML</Button>
        <Button className={"w-80"} color={"secondary"}
                onClick={() => download({dateStart, dateEnd, date, fileType: "JSON", entity})}>Download
            JSON</Button>
        <Button className={"w-80"} color={"secondary"}
                onClick={() => download({dateStart, dateEnd, date, fileType: "PDF", entity})}>Download
            PDF</Button>
    </div>
}

export default DownloadSection