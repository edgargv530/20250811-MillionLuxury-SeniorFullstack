import { useState, useEffect } from "react";
import Card from "../../components/molecules/Card";
import { fetchOwners } from "./ownersRepository.js";
import Grid from "../../components/organisms/Grid";

export default function Owners() {
	const [isFetching, setIsFetching] = useState(true);
	const [responseOwners, setResponse] = useState([]);
	const [error, setError] = useState();

	const columns = ['Id', 'Name', 'Address'];

	async function fetchData(top, skip) {
		try {
			setIsFetching(true);
			const owners = await fetchOwners(`$top=${top}&$skip=${skip}`);
			setResponse(owners);
		} catch (err) {
			setError({ message: err.message || 'Could not fetch owners' });
		} finally {
			setIsFetching(false);
		}
	}

	useEffect(() => {
		fetchData(5, 0);
	}, []);


	function handleChangeState(state) {
		//console.log(`Change state: ${state.top}`);
		fetchData(state.top, state.skip);
	}

	return (
		<Card title="Owners">
			{isFetching && (<p>Cargando</p>)}
			{!isFetching && error && (<p className="text-danger">{error.message}</p>)}
			{!isFetching && !error && (
				<Grid columns={columns} response={responseOwners} onChangeState={handleChangeState}></Grid>
			)}
		</Card>
	);
}