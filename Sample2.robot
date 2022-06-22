Welcome Page Should Be Open
    Verify Screen Breadcrumbs 'Configuration > System structures > Accounts > Calculated accounts'
    Get Title 'workflow'
    Click id=btnAddList
    Wait For Elements State id=modalWorkflowListContent visible
    Fill Text id=WorkflowListName Test Workflow List Name
    Click id=modalWorkflowListBtnSave
    Get Text id=WorkflowLists *= Test Workflow List Name