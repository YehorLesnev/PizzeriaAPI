"use client"
import {useAppDispatch, useAppSelector} from "@/hooks/useStore";
import {useEffect} from "react";
import {getProducts, getProductsCount} from "@/store/services/productService";
import ProductItem from "@/components/products/product-item";
import {Loader} from "@/components/loader/loader";
import ProductPagination from "@/components/pagination/product-pagination";
import {setTotalCount} from "@/store/features/product/productSlice";

interface ProductListProps {
    entity: string
}

const ProductList = ({entity}: ProductListProps) => {
    const dispatch = useAppDispatch();
    const {products, limit, activePage} = useAppSelector(
        (state) => state.product,
    );

    console.log(products)

    useEffect(() => {
        const getProductsCounter = async () => {
            const res =  getProductsCount({pageNumber: null, pageSize: null, itemCategory: entity})
             res.then(res => {
                 let count = 0
                 res.map(() => count++)

                 dispatch(setTotalCount(count))
             })
        }
        getProductsCounter()
    }, []);

    useEffect(() => {
        dispatch(
            getProducts({pageNumber: activePage, pageSize: limit, itemCategory: entity || null}),
        );
    }, [dispatch, activePage, entity, limit]);




    return (<>
            {products ? <div>
                <div
                    className={
                        "grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4 m-4 ml-0 min-h-[70vh] w-full"
                    }
                >
                    {products.map((product) => <ProductItem key={product.itemId} product={product}/>)}
                </div>

            </div> : <div className={"flex flex-col items-center justify-center w-full h-[73.3vh]"}><Loader/></div> }
            <ProductPagination/>

        </>
    );
};

export default ProductList;
