export default function Pagination({ pageNumber, totalPages, itemsPerPage, onChangePage, onChangeItemsPerPage }) {
	const liPages = [];
	const itemPerPageOptions = [5, 10, 20, 50];

	function handlePage(page) {
		onChangePage(page);
	}

	function handleItemsPerPage(event) {
		onChangeItemsPerPage(parseInt(event.target.value));
	}

	for (let i = 1; i <= totalPages; i++) {
		liPages.push(
			<li key={i} className={`page-item ${i === pageNumber ? "disabled" : ""}`} onClick={() => handlePage(i)}>
				<a className="page-link" href="#">{i}</a>
			</li>
		);
	}

	return (
		<div className="d-flex align-items-center justify-content-between">
			<div className="col-auto d-none d-sm-block">{`Page ${pageNumber} of ${totalPages}`}</div>
			<div className="row g-3 align-items-center">
				<div className="col-auto d-none d-sm-block">
					<label>Items per page</label>
				</div>
				<div className="col-auto">
					<select id="itemsPerPageSelect" className="form-select form-select-sm" aria-label="Items per page" defaultValue={itemsPerPage} onChange={handleItemsPerPage}>
						{itemPerPageOptions.map((option) => (<option key={option} value={option}>{option}</option>))}
					</select>
				</div>
			</div>
			<nav aria-label="Page navigation example">
				<ul className="pagination">
					<li className={`page-item ${pageNumber === 1 ? "disabled" : ""}`} onClick={() => handlePage(1)}>
						<a className="page-link" href="#" aria-label="Previous">
							<span aria-hidden="true">&laquo;</span>
						</a>
					</li>
					{liPages}
					<li className={`page-item ${pageNumber === totalPages ? "disabled" : ""}`} onClick={() => handlePage(totalPages)}>
						<a className="page-link" href="#" aria-label="Next">
							<span aria-hidden="true">&raquo;</span>
						</a>
					</li>
				</ul>
			</nav>
		</div>
	);
}