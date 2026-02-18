# Placeholder for guidance steps to register plugins using Plugin Registration Tool
# 1) Build XrmSample.Plugins for Release (net462)
# 2) Open Plugin Registration Tool (PRT) or Power Platform Tools in VS
# 3) Register new assembly: select XrmSample.Plugins.dll
# 4) Add Steps:
#    - AccountCreateValidationPlugin: Message=Create, Primary Entity=account, Stage=PreOperation, Synchronous
#    - FollowUpTaskPostCreatePlugin:  Message=Create, Primary Entity=account, Stage=PostOperation, Asynchronous
