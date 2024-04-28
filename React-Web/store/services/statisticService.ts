import {FetchProductsParams} from "@/store/services/productService";
import {instance} from "@/api/axiosInstance";
import {DateValue} from "@internationalized/date";

export interface IStatisticParams {
    dateStart?: string | DateValue,
    dateEnd?: string | DateValue
}

export interface IGet extends IStatisticParams {
    date?: string
    itemCategory?: "Pizza" | "Hotdog" | "Fries" | "Burger"
    uri?: string
    entity: string
    fileType?: string

}
export const getStaffPayroll = async ({dateStart, dateEnd}: IStatisticParams) => {
    console.log(dateStart, dateEnd)
    try {
        const params: IStatisticParams = {}
        if (dateStart) params.dateStart = dateStart
        if (dateEnd) params.dateEnd = dateEnd
        const response = await instance.get("/Statistics/GetStaffPayroll", {params}  );
        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}



export const downloadXMLPayroll = async ({dateStart, dateEnd}: IStatisticParams) => {
    try {
        const params: IStatisticParams = {}
        if (dateStart) params.dateStart = dateStart
        if (dateEnd) params.dateEnd = dateEnd
        const response = await instance.get("/Downloads/XML/StaffPayroll", {params}  );
        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}
export const getStatistics = async ({date, dateStart, dateEnd, uri, entity, itemCategory}: IGet) => {
    console.log(dateStart, dateEnd)
    try {
        const params: any = {}
        if (dateStart) params.dateStart = dateStart
        if (dateEnd) params.dateEnd = dateEnd
        if (itemCategory) params.itemCategory = itemCategory
        if (date) params.date = date
        const response = await instance.get(`/Statistics/${uri}/${entity}`, {params}  );
        return response.data;
    } catch (error) {
        console.error("Error while checking authentication:", error);
    }
}


export const getDownloads = async ({ date, dateStart, dateEnd, fileType, entity, itemCategory }: IGet) => {
    console.log(dateStart, dateEnd); // Good for debugging
    try {
        const params: any = {};
        if (dateStart) params.dateStart = dateStart;
        if (dateEnd) params.dateEnd = dateEnd;
        if (itemCategory) params.itemCategory = itemCategory;
        if (date) params.date = date;

        // Setting responseType based on fileType
        const responseType = fileType === 'PDF' ? 'blob' : 'json';

        const response = await instance.get(`/Downloads/${fileType}/${entity}`, {
            params,
            responseType: responseType  // Important for handling binary data like PDF
        });

        return response.data;
    } catch (error) {
        console.error("Error while fetching downloads:", error);
        return null; // Return null or appropriate error handling
    }
}

