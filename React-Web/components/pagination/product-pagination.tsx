"use client"
import {Pagination} from "@nextui-org/pagination";
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {setActivePage} from "@/store/features/product/productSlice";

const ProductPagination = () => {

    const {totalCount, activePage, limit} = useAppSelector(state => state.product)
    const dispatch = useAppDispatch()
    const pageCount = Math.ceil( totalCount / limit);

    return (
        <div className="flex flex-col w-full justify-center items-center gap-5 ">
            <Pagination
                total={pageCount}
                color="primary"
                page={activePage}
                onChange={e => dispatch(setActivePage(e))}
            />

        </div>)
}

export default ProductPagination