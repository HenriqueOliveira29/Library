import React, { useState, useEffect } from 'react'
import { BookService } from '../services/BookService'
import { ListBookDTO } from "../models/books/ListBookDTO";
import { MessagingHelper } from "../helpers/MessagingHelper";


function BooksIndex() {
  const [data, setData] = useState<ListBookDTO[]>([]);
  const service = new BookService();
  const [reloadSearch, setReloadSearch] = useState<boolean>(false);

  useEffect(() => {
    if (reloadSearch === true) {
      fetchData();
      setReloadSearch(false);
    }
    console.log(data)
  }, [reloadSearch]);

  const fetchData = async () => {
    const response = await service.GetAll().then(result => {
      setData(result.obj);
    });
  }

  return (
    <div>
      {
        data.map((element) => {
          <div>{element.name}</div>
        })
      }
    </div>

  )
}

export default BooksIndex