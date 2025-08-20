import { useState, useEffect } from "react";
import Card from "../../components/molecules/Card";
import { fetchOwners } from "./ownersRepository.js";
import Grid from "../../components/molecules/Grid";

export default function Owners() {
	const [isFetching, setIsFetching] = useState(true);
	const [responseOwners, setResponse] = useState([]);
	const [error, setError] = useState();

	const columns = ['Id', 'Name', 'Address'];

	useEffect(() => {
		async function fetchData() {
			try {
				setIsFetching(true);
				const owners = await fetchOwners(`$top=5&$skip=0&$orderby=name`);
				setResponse(owners);
			} catch (err) {
				setError({ message: err.message || 'Could not fetch owners' });
			} finally {
				setIsFetching(false);
			}
		}

		fetchData();
	}, []);

	return (
		<Card title="Owners">
			{isFetching && (<p>Cargando</p>)}
			{!isFetching && error && (<p>{error.message}</p>)}
			{!isFetching && !error && (<Grid columns={columns} response={responseOwners}></Grid>)}
		</Card>
	);
}